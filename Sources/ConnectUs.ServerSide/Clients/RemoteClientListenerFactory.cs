using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide.Clients
{
    public class RemoteClientListenerFactory
    {
        public IRemoteClientListener Build()
        {
            return new RemoteClientListener(new TcpClientConnectionListener());
        }
    }
}