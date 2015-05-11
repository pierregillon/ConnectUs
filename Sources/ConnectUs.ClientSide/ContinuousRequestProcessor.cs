using System;
using System.Threading;
using ConnectUs.Business;
using ConnectUs.Business.Connections;
using Newtonsoft.Json;

namespace ConnectUs.ClientSide
{
    public class ContinuousRequestProcessor
    {
        private const int DelayBeforeNewConnectionRead = 1000;
        private readonly IRequestProcessor _requestProcessor;
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private bool _continueProcessing = true;

        public event EventHandler ConnectionLost;
        protected virtual void OnConnectionLost()
        {
            EventHandler handler = ConnectionLost;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        // ----- Constructors
        public ContinuousRequestProcessor(IRequestProcessor requestProcessor)
        {
            _requestProcessor = requestProcessor;
        }

        // ----- Public methods
        public void StartProcessingRequestFromConnection(IConnection connection)
        {
            _continueProcessing = true;
            var thread = new Thread(() => ExecuteMultipleRequestOnConnection(connection));
            thread.Start();
        }
        public void StopProcessingRequestFromConnection()
        {
            _continueProcessing = false;
            _resetEvent.WaitOne();
        }

        // ----- Internal logics
        private void ExecuteMultipleRequestOnConnection(IConnection connection)
        {
            try {
                while (_continueProcessing) {
                    ExecuteRequestOnConnection(connection);
                }
                _resetEvent.Set();
            }
            catch (ConnectionException) {
                OnConnectionLost();
            }
        }
        private void ExecuteRequestOnConnection(IConnection connection)
        {
            try {
                var jsonRequest = connection.Read();
                var request = JsonConvert.DeserializeObject<Request>(jsonRequest);
                var response = _requestProcessor.Process(request);
                var jsonResponse = JsonConvert.SerializeObject(response);
                connection.Send(jsonResponse);
            }
            catch (NoDataToReadFromConnectionException) {
                Thread.Sleep(DelayBeforeNewConnectionRead);
            }
        }
    }

    public class ClientRequestHandler : IClientRequestHandler
    {
        private readonly IClientRequestProcessor _clientRequestProcessor;
        private readonly IRequestParser _requestParser;

        // ----- Constructors
        public ClientRequestHandler(IClientRequestProcessor clientRequestProcessor, IRequestParser requestParser)
        {
            _clientRequestProcessor = clientRequestProcessor;
            _requestParser = requestParser;
        }

        // ----- Public methods
        public void ProcessNextRequestFrom(IConnection connection)
        {
            try {
                var jsonRequest = connection.Read();
                var requestName = _requestParser.GetRequestName(jsonRequest);
                var response = _clientRequestProcessor.Process(requestName, jsonRequest);
                var jsonResponse = JsonConvert.SerializeObject(response);
                connection.Send(jsonResponse);
            }
            catch (NoDataToReadFromConnectionException ex) {
                throw new NoRequestToProcessException("No request is available to process.", ex);
            }
        }
    }

    public interface IClientRequestHandler
    {
        void ProcessNextRequestFrom(IConnection connection);
    }



    public interface IClientRequestProcessor
    {
        object Process(string requestName, string originalData);
    }
}