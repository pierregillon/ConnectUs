using System.Net;

namespace ConnectUs.ClientSide
{
    public interface IClientInformationService
    {
        IPAddress GetIp();
        string GetMachineName();
    }
}