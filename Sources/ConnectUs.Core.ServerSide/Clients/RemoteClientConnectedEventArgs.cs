using System;

namespace ConnectUs.Core.ServerSide.Clients
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