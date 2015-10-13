using ConnectUs.Core.Connections;

namespace ConnectUs.Core.ClientSide
{
    public class ClientRequestHandler : IClientRequestHandler
    {
        private readonly IClientRequestProcessor _clientRequestProcessor;

        // ----- Constructors
        public ClientRequestHandler(IClientRequestProcessor clientRequestProcessor)
        {
            _clientRequestProcessor = clientRequestProcessor;
        }

        // ----- Public methods
        public void ProcessNextRequestFrom(IConnection connection)
        {
            try {
                var request = connection.Read();
                var response = _clientRequestProcessor.Process(request);
                connection.Send(response);
            }
            catch (NoDataToReadFromConnectionException ex) {
                throw new NoRequestToProcessException("No request is available to process.", ex);
            }
        }
    }
}