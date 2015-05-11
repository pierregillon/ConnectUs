using ConnectUs.Business;
using ConnectUs.Business.Connections;
using Newtonsoft.Json;

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
                var jsonRequest = connection.Read();
                var requestName = _requestParser.GetRequestName(jsonRequest);
                var response = _clientRequestProcessor.Process(requestName, jsonRequest);
                var jsonResponse = JsonConvert.SerializeObject(response);
                connection.Send(jsonResponse);
            }
            catch (NoDataToReadFromConnectionException ex) {
                throw new NoRequestToProcessException("No request is available to process.", ex);
            }
        }
    }
}