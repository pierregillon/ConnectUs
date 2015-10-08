using ConnectUs.ClientSide.Commands.GetClientInformation;
using ConnectUs.ClientSide.Commands.Ping;

namespace ConnectUs.ServerSide.Business
{
    public class ClientInformationDecorator
    {
        private readonly RemoteClient _remoteClient;

        public ClientInformationDecorator(RemoteClient remoteClient)
        {
            _remoteClient = remoteClient;
        }

        public GetClientInformationResponse GetClientInformation()
        {
            return _remoteClient.Send<GetClientInformationRequest, GetClientInformationResponse>(new GetClientInformationRequest());
        }

        public void Ping()
        {
            try {
                var response = _remoteClient.Send<PingRequest, PingResponse>(new PingRequest());
                if (response.Value != "OK") {
                    throw new ClientException("An error occured during a ping. The value is invalid.");
                }
            }
            catch (RequestException ex) {
                throw new ClientException("An error occured during the ping request", ex);
            }
        }
    }
}