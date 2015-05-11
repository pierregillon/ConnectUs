using System;
using System.Net;

namespace ConnectUs.Business.Commands
{
    public class ClientInformationService : IClientInformationService
    {
        public IPAddress GetIp()
        {
            return IPAddress.Parse("0.0.0.0");
        }
        public string GetMachineName()
        {
            return Environment.MachineName;
        }
    }
}