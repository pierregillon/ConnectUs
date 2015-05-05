using ConnectUs.Business;
using ConnectUs.ClientSide;

namespace ConnectUs.ServerSide
{
    public class Client
    {
        private readonly ClientSide.Client _client;

        public Client(ClientSide.Client client)
        {
            _client = client;
        }

        public ClientInformationResponse GetClientInformation()
        {
            var response = _client.Execute(new Request {Name = "GetClientInformation"});
            return response.To<ClientInformationResponse>();
        }
    }
}