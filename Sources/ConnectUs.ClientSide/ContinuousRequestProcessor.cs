using System;
using System.Threading;
using ConnectUs.Business;
using ConnectUs.Business.Connections;

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
                var request = connection.Read<Request>();
                var response = _requestProcessor.Process(request);
                connection.Send(response);
            }
            catch (NoDataToReadFromConnectionException) {
                Thread.Sleep(DelayBeforeNewConnectionRead);
            }
        }
    }
}