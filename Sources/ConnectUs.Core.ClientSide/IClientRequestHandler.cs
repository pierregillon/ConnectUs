using ConnectUs.Core.Connections;

namespace ConnectUs.Core.ClientSide
{
    public interface IClientRequestHandler
    {
        void ProcessNextRequestFrom(IConnection connection);
    }
}