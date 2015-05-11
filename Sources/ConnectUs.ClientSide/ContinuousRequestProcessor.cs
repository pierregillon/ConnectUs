using System;
using System.Threading;
using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public class ContinuousRequestProcessor
    {
        private const int DelayBeforeNewConnectionRead = 1000;
        private readonly IClientRequestHandler _clientRequestHandler;
        private bool _continueProcessing = true;

        public event EventHandler ConnectionLost;
        protected virtual void OnConnectionLost()
        {
            EventHandler handler = ConnectionLost;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        // ----- Constructors
        public ContinuousRequestProcessor(IClientRequestProcessor clientRequestProcessor)
        {
            _clientRequestHandler = new ClientRequestHandler(clientRequestProcessor, new JsonRequestParser());
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
        }

        // ----- Internal logics
        private void ExecuteMultipleRequestOnConnection(IConnection connection)
        {
            try {
                while (_continueProcessing) {
                    try {
                        _clientRequestHandler.ProcessNextRequestFrom(connection);
                    }
                    catch (NoRequestToProcessException) {
                        Thread.Sleep(DelayBeforeNewConnectionRead);
                    }
                }
            }
            catch (ConnectionException) {
                OnConnectionLost();
            }
        }
    }
}