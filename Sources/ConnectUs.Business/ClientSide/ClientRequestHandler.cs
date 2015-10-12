using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public class ClientRequestHandler : IClientRequestHandler
    {
        private readonly IClientRequestProcessor _clientRequestProcessor;
        private readonly IRequestParser _requestParser;

        // ----- Constructors
        public ClientRequestHandler(IClientRequestProcessor clientRequestProcessor, IRequestParser requestParser)
        {
            _clientRequestProcessor = clientRequestProcessor;
            _requestParser = requestParser;
        }

        // ----- Public methods
        public void ProcessNextRequestFrom(IConnection connection)
        {
            try {
                var request = connection.Read();
                var requestName = _requestParser.GetRequestName(request);
                var response = _clientRequestProcessor.Process(requestName, request);
                connection.Send(response);
            }
            catch (NoDataToReadFromConnectionException ex) {
                throw new NoRequestToProcessException("No request is available to process.", ex);
            }
        }
    }
}