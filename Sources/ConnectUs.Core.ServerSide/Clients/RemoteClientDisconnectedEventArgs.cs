using System;

namespace ConnectUs.Core.ServerSide.Clients
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