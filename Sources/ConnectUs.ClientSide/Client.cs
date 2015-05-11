using System;
using System.Threading;
using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public class Client
    {
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private readonly ContinuousRequestProcessor _continuousRequestProcessor = new ContinuousRequestProcessor(new RequestProcessor(new ClientInformationService()));

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
            _continuousRequestProcessor.ConnectionLost += ContinuousRequestProcessorOnConnectionLost;
        }

        // ----- Public methods
        public void ConnectToServer(string hostName, int port)
        {
            var connection = GetConnection(hostName, port);
            if (connection != null) {
                _continuousRequestProcessor.StartProcessingRequestFromConnection(connection);
                _resetEvent.WaitOne();
            }
        }

        // ----- Utils
        private IConnection GetConnection(string hostName, int port)
        {
            try {
                var connection = TcpClientConnectionFactory.Build(hostName, port);
                OnClientConnected();
                return connection;
            }
            catch (ConnectionException) {
                return null;
            }
        }

        // ----- Event callbacks
        private void ContinuousRequestProcessorOnConnectionLost(object sender, EventArgs args)
        {
            OnClientDisconnected();
            _resetEvent.Set();
        }
    }
}