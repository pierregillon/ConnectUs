using System;

namespace ConnectUs.Core.ServerSide.Clients
{
    public interface IRemoteClientListener
    {
        event EventHandler<RemoteClientConnectedEventArgs> ClientConnected;
        event EventHandler<RemoteClientDisconnectedEventArgs> ClientDisconnected;

        void Start(int port);
        void Stop();
    }
}