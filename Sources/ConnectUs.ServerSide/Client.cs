using ConnectUs.Common.GetClientInformation;
using ConnectUs.Common.Ping;

namespace ConnectUs.ServerSide
{
    public class Client
    {
        private readonly IServerRequestProcessor _serverRequestProcessor;

        public Client(IServerRequestProcessor serverRequestProcessor)
        {
            _serverRequestProcessor = serverRequestProcessor;
        }

        public GetClientInformationResponse GetClientInformation()
        {
            return _serverRequestProcessor.Process<GetClientInformationRequest, GetClientInformationResponse>(new GetClientInformationRequest());
        }
        public void CloseConnection()
        {
            _serverRequestProcessor.Close();
        }
        public void Ping()
        {
            try {
                var response = _serverRequestProcessor.Process<PingRequest, PingResponse>(new PingRequest());
                if (response.Value != "OK") {
                    throw new ClientException("An error occured during a ping. The value is invalid.");
                }
            }
            catch (RequestException ex) {
                throw new ClientException("An error occured during the ping request", ex);
            }
        }
        public TResponse ExecuteCommand<TRequest, TResponse>(TRequest request)
        {
            return _serverRequestProcessor.Process<TRequest, TResponse>(request);
        }
    }
}