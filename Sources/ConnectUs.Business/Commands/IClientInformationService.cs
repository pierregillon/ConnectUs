using System.Net;

namespace ConnectUs.Business.Commands
{
    public interface IClientInformationService
    {
        IPAddress GetIp();
        string GetMachineName();
    }
}