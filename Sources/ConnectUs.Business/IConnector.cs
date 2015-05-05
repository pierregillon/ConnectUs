using System;
using System.Collections.Generic;

namespace ConnectUs.Business
{
    public interface IConnector
    {
        event EventHandler<ConnectionEstablishedEventArgs> ClientConnected;
        void Connect(string host, int port);
        void Listen(int port);
    }

    public class ConnectionEstablishedEventArgs : EventArgs {}

    public class FakeConnector : IConnector
    {
        private readonly Dictionary<int, List<string>> _ports = new Dictionary<int, List<string>>();

        public void Connect(string host, int port)
        {
            _ports[port].Add("toto");
        }
        public void Listen(int port)
        {
            _ports.Add(port, new List<string>());
        }
    }
}