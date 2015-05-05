using System;
using ConnectUs.ServerSide;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeClientListener : IClientListener
    {
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        protected virtual void OnClientConnected(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = ClientConnected;
            if (handler != null) handler(this, e);
        }

        public void Start()
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
    }
}
