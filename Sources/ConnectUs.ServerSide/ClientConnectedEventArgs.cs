using System;

namespace ConnectUs.ServerSide
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public Client Client { get; private set; }
        public ClientConnectedEventArgs(Client client)
        {
            Client = client;
        }
    }
}