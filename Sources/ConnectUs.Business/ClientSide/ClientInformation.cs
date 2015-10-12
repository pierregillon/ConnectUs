using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public class ClientInformation : IClientInformation
    {
        public IConnection CurrentConnection { get; set; }
    }
}