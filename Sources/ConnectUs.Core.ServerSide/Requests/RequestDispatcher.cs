using ConnectUs.Core.Connections;

namespace ConnectUs.Core.ServerSide.Requests
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IConnection _connection;
        private readonly IRequestParser _requestParser;

        public RequestDispatcher(IConnection connection, IRequestParser requestParser)
        {
            _connection = connection;
            _requestParser = requestParser;
        }

        public void SendRequest<TRequest>(TRequest request)
        {
            try {
                var data = _requestParser.ConvertToBytes(request);
                _connection.Send(data);
            }
            catch (ConnectionException ex) {
                throw new RequestException("Unable to send the request.", ex);
            }
        }
        public TResponse ReceiveResponse<TResponse>()
        {
            try {
                var data = _connection.Read();
                var error =_requestParser.GetError(data);
                if (string.IsNullOrEmpty(error) == false) {
                    throw new RequestException(error);
                }
                return _requestParser.FromBytes<TResponse>(data);
            }
            catch (ConnectionException ex) {
                throw new RequestException("Unable to receive the request.", ex);
            }
        }
        public void SendFile(string filePath)
        {
            var uploader = new Uploader(_connection);
            uploader.Upload(filePath);
        }
        public void ReceiveFile(string filePath)
        {
            var downloader = new Downloader(_connection);
            downloader.Download(filePath);
        }
        public void Close()
        {
            _connection.Close();
        }
    }
}