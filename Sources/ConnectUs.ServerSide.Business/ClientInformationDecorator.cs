using ConnectUs.ClientSide.Commands.GetClientInformation;
using ConnectUs.ClientSide.Commands.Ping;

namespace ConnectUs.ServerSide.Business
{
    public class ClientInformationDecorator
    {
        private readonly Client _client;

        public ClientInformationDecorator(Client client)
        {
            _client = client;
        }

        public GetClientInformationResponse GetClientInformation()
        {
            return _client.ExecuteCommand<GetClientInformationRequest, GetClientInformationResponse>(new GetClientInformationRequest());
        }

        public void Ping()
        {
            try {
                var response = _client.ExecuteCommand<PingRequest, PingResponse>(new PingRequest());
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