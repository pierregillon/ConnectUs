using System;

namespace ConnectUs.Core.Connections
{
    public class ConnectionLostEventArgs : EventArgs
    {
        public IConnection Connection { get; private set; }
        public ConnectionLostEventArgs(IConnection connection)
        {
            Connection = connection;
        }
    }
}