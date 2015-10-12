using System;
using ConnectUs.Core.ServerSide.Clients;

namespace ConnectUs.Core.Tests.Mocks
{
    public class FakeRemoteClientListener : IRemoteClientListener
    {
        public event EventHandler<RemoteClientConnectedEventArgs> ClientConnected;
        public event EventHandler<RemoteClientDisconnectedEventArgs> ClientDisconnected;

        protected virtual void OnClientDisconnected(RemoteClientDisconnectedEventArgs e)
        {
            EventHandler<RemoteClientDisconnectedEventArgs> handler = ClientDisconnected;
            if (handler != null) handler(this, e);
        }
        protected virtual void OnClientConnected(RemoteClientConnectedEventArgs e)
        {
            EventHandler<RemoteClientConnectedEventArgs> handler = ClientConnected;
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

        public void AddClient(IRemoteClient remoteClient)
        {
            OnClientConnected(new RemoteClientConnectedEventArgs(remoteClient));
        }
        public void RemoveClient(IRemoteClient remoteClient)
        {
            OnClientDisconnected(new RemoteClientDisconnectedEventArgs(remoteClient));
        }
    }
}