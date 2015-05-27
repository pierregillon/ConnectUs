using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class ServerRequestCommunicator : IServerRequestCommunicator
    {
        private readonly IConnection _connection;
        private readonly IRequestParser _requestParser;

        public ServerRequestCommunicator(IConnection connection, IRequestParser requestParser)
        {
            _connection = connection;
            _requestParser = requestParser;
        }

        public void SendToClient<TRequest>(TRequest request)
        {
            try {
                var jsonRequest = _requestParser.ConvertToBytes(request);
                _connection.Send(jsonRequest);
            }
            catch (ConnectionException ex) {
                throw new RequestException("Unable to send the request.", ex);
            }
        }
        public TResponse ReceiveFromClient<TResponse>()
        {
            try {
                var jsonResponse = _connection.Read();
                var error =_requestParser.GetError(jsonResponse);
                if (string.IsNullOrEmpty(error) == false) {
                    throw new RequestException(error);
                }
                return _requestParser.FromBytes<TResponse>(jsonResponse);
            }
            catch (ConnectionException ex) {
                throw new RequestException("Unable to receive the request.", ex);
            }
        }
        public void Close()
        {
            _connection.Close();
        }
    }
}