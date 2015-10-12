using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public interface IClientRequestHandler
    {
        void ProcessNextRequestFrom(IConnection connection);
    }
}