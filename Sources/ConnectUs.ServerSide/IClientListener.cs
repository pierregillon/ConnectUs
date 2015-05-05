using System;

namespace ConnectUs.ServerSide
{
    public interface IClientListener
    {
        event EventHandler<ClientConnectedEventArgs> ClientConnected;
        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        void Start();
        void Stop();
    }
}