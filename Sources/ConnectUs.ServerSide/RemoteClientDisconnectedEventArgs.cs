using System;

namespace ConnectUs.ServerSide
{
    public class RemoteClientDisconnectedEventArgs : EventArgs
    {
        public IRemoteClient RemoteClient { get; private set; }
        public RemoteClientDisconnectedEventArgs(IRemoteClient remoteClient)
        {
            RemoteClient = remoteClient;
        }
    }
}