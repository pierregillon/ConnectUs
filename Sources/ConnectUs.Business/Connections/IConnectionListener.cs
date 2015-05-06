using System;

namespace ConnectUs.Business.Connections
{
    public interface IConnectionListener
    {
        event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;
        event EventHandler<ConnectionLostEventArgs> ConnectionLost;

        void Start(int port);
        void Stop();
    }
}