using System;
using ConnectUs.ServerSide;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeClientListener : IClientListener
    {
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        protected virtual void OnClientDisconnected(ClientDisconnectedEventArgs e)
        {
            EventHandler<ClientDisconnectedEventArgs> handler = ClientDisconnected;
            if (handler != null) handler(this, e);
        }
        protected virtual void OnClientConnected(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = ClientConnected;
            if (handler != null) handler(this, e);
        }

        public void Start(int port)
        {
            throw new NotImplementedException();
        }
        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void AddClient(Client client)
        {
            OnClientConnected(new ClientConnectedEventArgs(client));
        }
        public void RemoveClient(Client client)
        {
            OnClientDisconnected(new ClientDisconnectedEventArgs(client));
        }
    }
}