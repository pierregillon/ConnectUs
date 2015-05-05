using ConnectUs.Business;
using ConnectUs.ClientSide;

namespace ConnectUs.ServerSide
{
    public class Client
    {
        private readonly IRequestProcessor _requestProcessor;

        public Client(IRequestProcessor requestProcessor)
        {
            _requestProcessor = requestProcessor;
        }

        public ClientInformationResponse GetClientInformation()
        {
            var response = _requestProcessor.Process(new Request {Name = "GetClientInformation"});
            return response.To<ClientInformationResponse>();
        }
    }
}