using System;

namespace ConnectUs.ServerSide
{
    public class RemoteClientConnectedEventArgs : EventArgs
    {
        public RemoteClient RemoteClient { get; private set; }
        public RemoteClientConnectedEventArgs(RemoteClient remoteClient)
        {
            RemoteClient = remoteClient;
        }
    }
}