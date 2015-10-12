using ConnectUs.Core.Connections;

namespace ConnectUs.Core.ClientSide
{
    public interface IClientInformation
    {
        IConnection CurrentConnection { get; set; }
    }
}