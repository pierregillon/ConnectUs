using System;

namespace ConnectUs.ServerSide
{
    public class RemoteClientConnectedEventArgs : EventArgs
    {
        public IRemoteClient RemoteClient { get; private set; }
        public RemoteClientConnectedEventArgs(IRemoteClient remoteClient)
        {
            RemoteClient = remoteClient;
        }
    }
}