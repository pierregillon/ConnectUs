using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public interface IClientInformation
    {
        IConnection CurrentConnection { get; set; }
    }
}