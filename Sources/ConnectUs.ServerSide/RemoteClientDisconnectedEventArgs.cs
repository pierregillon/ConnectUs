using System;

namespace ConnectUs.ServerSide
{
    public class RemoteClientDisconnectedEventArgs : EventArgs
    {
        public RemoteClient RemoteClient { get; private set; }
        public RemoteClientDisconnectedEventArgs(RemoteClient remoteClient)
        {
            RemoteClient = remoteClient;
        }
    }
}