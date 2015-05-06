using System;

namespace ConnectUs.Business.Connections
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