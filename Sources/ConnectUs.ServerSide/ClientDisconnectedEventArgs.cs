using System;

namespace ConnectUs.ServerSide
{
    public class ClientDisconnectedEventArgs : EventArgs
    {
        public string Reason { get; set; }
    }
}