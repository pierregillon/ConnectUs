using ConnectUs.Business;
using ConnectUs.Business.Connections;

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
        public void Ping()
        {
            try {
                var response = _requestProcessor.Process(new Request {Name = "Ping"});
                if (response.Error != null) {
                    throw new ClientException(response.Error);
                }
            }
            catch (ConnectionException ex) {
                throw new ClientException("Unable to execute the command 'Ping', the connection has been closed.", ex);
            }
        }
    }
}