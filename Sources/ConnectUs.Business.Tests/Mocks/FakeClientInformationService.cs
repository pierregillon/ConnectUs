using System.Net;
using ConnectUs.ClientSide;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeClientInformationService : IClientInformationService
    {
        private readonly IPAddress _ipAddress;

        public FakeClientInformationService(IPAddress ipAddress)
        {
            _ipAddress = ipAddress;
        }

        public IPAddress GetIp()
        {
            return _ipAddress;
        }
    }
}