using System;
using System.Threading;
using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public class ContinuousRequestProcessor : IContinuousRequestProcessor
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
        public ContinuousRequestProcessor(IClientRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        // ----- Public methods
        public void StartProcessingRequestFromConnection(IConnection connection)
        {
            _continueProcessing = true;
            var thread = new Thread(() => ProcessRequestFrom(connection));
            thread.Start();
        }
        public void StopProcessingRequestFromConnection()
        {
            _continueProcessing = false;
        }

        // ----- Internal logics
        private void ProcessRequestFrom(IConnection connection)
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