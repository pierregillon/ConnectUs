using System.Diagnostics;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Modules.Integrated.GetClientInformation;
using ConnectUs.Modules.Integrated.GetFullClientInformation;
using ConnectUs.Modules.Integrated.Ping;

namespace ConnectUs.ServerSide.Business
{
    internal class ClientInformationDecorator
    {
        private readonly IRemoteClient _remoteClient;
        private readonly Stopwatch _watch = new Stopwatch();

        public ClientInformationDecorator(IRemoteClient remoteClient)
        {
            _remoteClient = remoteClient;
        }

        public GetClientInformationResponse GetClientInformation()
        {
            return _remoteClient.Send<GetClientInformationRequest, GetClientInformationResponse>(new GetClientInformationRequest());
        }

        public GetFullClientInformationResponse GetFullClientInformation()
        {
            return _remoteClient.Send<GetFullClientInformationRequest, GetFullClientInformationResponse>(new GetFullClientInformationRequest());
        }
        public int Ping()
        {
            try {
                _watch.Start();
                var response = _remoteClient.Send<PingRequest, PingResponse>(new PingRequest());
                if (response.Value != "OK") {
                    throw new RemoteClientException("An error occured during a ping. The value is invalid.");
                }
                _watch.Stop();
                return (int) _watch.ElapsedMilliseconds;
            }
            finally {
                _watch.Reset();
            }
        }
    }
}