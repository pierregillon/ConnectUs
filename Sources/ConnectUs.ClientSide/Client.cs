using System;
using ConnectUs.Business;
using ConnectUs.Business.Connections;
using ConnectUs.ClientSide.ModuleManagement;

namespace ConnectUs.ClientSide
{
    public class Client
    {
        private readonly ContinuousRequestProcessor _continuousRequestProcessor;

        public event EventHandler ClientDisconnected;
        protected virtual void OnClientDisconnected()
        {
            EventHandler handler = ClientDisconnected;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler ClientConnected;
        protected virtual void OnClientConnected()
        {
            EventHandler handler = ClientConnected;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        // ----- Constructors
        public Client()
        {
            var moduleManager = new ModuleManager();
            _continuousRequestProcessor = new ContinuousRequestProcessor(new ClientRequestProcessor(new CommandLocator(moduleManager), new JsonRequestParser()));
            _continuousRequestProcessor.ConnectionLost += ContinuousRequestProcessorOnConnectionLost;
        }

        // ----- Public methods
        public void ConnectToServer(string hostName, int port)
        {
            try {
                var connection = TcpClientConnectionFactory.Build(hostName, port);
                OnClientConnected();
                _continuousRequestProcessor.StartProcessingRequestFromConnection(connection);
            }
            catch (ConnectionException) {
                throw new ClientException(string.Format("Unable to connect to the host '{0}' on the port '{1}'.", hostName, port));
            }
        }

        // ----- Event callbacks
        private void ContinuousRequestProcessorOnConnectionLost(object sender, EventArgs args)
        {
            _continuousRequestProcessor.StopProcessingRequestFromConnection();
            OnClientDisconnected();
        }
    }
}