using System.Diagnostics;
using System.Threading;
using ConnectUs.ClientSide.Commands.GetClientInformation;
using ConnectUs.ClientSide.Commands.Ping;

namespace ConnectUs.ServerSide.Business
{
    public class ClientInformationDecorator
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

        public int Ping()
        {
            try {
                _watch.Start();
                var response = _remoteClient.Send<PingRequest, PingResponse>(new PingRequest());
                if (response.Value != "OK") {
                    throw new ClientException("An error occured during a ping. The value is invalid.");
                }
                _watch.Stop();
                return (int) _watch.ElapsedMilliseconds;
            }
            catch (RequestException ex) {
                throw new ClientException("An error occured during the ping request", ex);
            }
            finally {
                _watch.Reset();
            }
        }
    }
}