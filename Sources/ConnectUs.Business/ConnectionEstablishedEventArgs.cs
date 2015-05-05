using System;

namespace ConnectUs.Business
{
    public class ConnectionEstablishedEventArgs : EventArgs
    {
        public IConnection Connection { get; private set; }

        public ConnectionEstablishedEventArgs(IConnection connection)
        {
            Connection = connection;
        }
    }
}