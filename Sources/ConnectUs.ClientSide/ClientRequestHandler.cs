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
                var dataRead = connection.Read();
                var requestName = _requestParser.GetRequestName(dataRead);
                var resultData = _clientRequestProcessor.Process(requestName, dataRead);
                connection.Send(resultData);
            }
            catch (NoDataToReadFromConnectionException ex) {
                throw new NoRequestToProcessException("No request is available to process.", ex);
            }
        }
    }
}