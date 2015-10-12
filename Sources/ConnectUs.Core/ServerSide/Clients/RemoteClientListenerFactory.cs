using ConnectUs.Core.Connections;

namespace ConnectUs.Core.ServerSide.Clients
{
    public class RemoteClientListenerFactory
    {
        public IRemoteClientListener Build()
        {
            return new RemoteClientListener(new TcpClientConnectionListener());
        }
    }
}