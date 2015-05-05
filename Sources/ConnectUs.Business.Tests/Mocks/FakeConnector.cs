using System;
using System.Collections.Generic;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeConnector : IConnector, IConnectionListener
    {
        private readonly Dictionary<int, List<FakeConnection>> _ports = new Dictionary<int, List<FakeConnection>>();

        public event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;
        protected virtual void OnClientConnected(ConnectionEstablishedEventArgs e)
        {
            var handler = ConnectionEstablished;
            if (handler != null) handler(this, e);
        }

        public IConnection Connect(string host, int port)
        {
            var connection = new FakeConnection();
            _ports[port].Add(connection);
            OnClientConnected(new ConnectionEstablishedEventArgs(connection));
            return connection;
        }
        public void StartListening(int port)
        {
            _ports.Add(port, new List<FakeConnection>());
        }
        public void StopListening()
        {
        }
    }
}