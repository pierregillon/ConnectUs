using ConnectUs.Core.Connections;

namespace ConnectUs.Core.ClientSide
{
    public class ClientInformation : IClientInformation
    {
        public IConnection CurrentConnection { get; set; }
    }
}