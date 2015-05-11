using System;
using System.Collections.Generic;

namespace ConnectUs.ServerSide
{
    public interface IServer
    {
        event EventHandler<ClientConnectedEventArgs> ClientConnected;
        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        IEnumerable<Client> GetConnectedClients();
        void Start();
        void Stop();
    }
}