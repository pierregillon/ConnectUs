using System.Net;
using ConnectUs.ClientSide;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeClientInformationService : IClientInformationService
    {
        private readonly IPAddress _ipAddress;
        private readonly string _machineName;

        public FakeClientInformationService(IPAddress ipAddress, string machineName)
        {
            _ipAddress = ipAddress;
            _machineName = machineName;
        }

        public IPAddress GetIp()
        {
            return _ipAddress;
        }
        public string GetMachineName()
        {
            return _machineName;
        }
    }
}