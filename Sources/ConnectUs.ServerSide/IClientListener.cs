using System;

namespace ConnectUs.ServerSide
{
    public interface IClientListener
    {
        event EventHandler<ClientConnectedEventArgs> ClientConnected;

        void Start();
        void Stop();
    }
}