using System;

namespace ConnectUs.Business
{
    public interface IConnectionListener
    {
        event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;

        void StartListening(int port);
        void StopListening();
    }
}