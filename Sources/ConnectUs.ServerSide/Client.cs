using ConnectUs.Business;

namespace ConnectUs.ServerSide
{
    public class Client
    {
        private readonly IRequestProcessor _requestProcessor;

        public Client(IRequestProcessor requestProcessor)
        {
            _requestProcessor = requestProcessor;
        }

        public ClientInformation GetClientInformation()
        {
            var response = _requestProcessor.Process(new Request {Name = "GetClientInformation"});
            if (response.Error != null) {
                throw new ClientException(response.Error);
            }
            return response.To<ClientInformation>();
        }
        public void CloseConnection()
        {
            _requestProcessor.Close();
        }
    }
}