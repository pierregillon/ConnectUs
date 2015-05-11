using System;
using ConnectUs.Business;
using ConnectUs.Business.Connections;
using Newtonsoft.Json;

namespace ConnectUs.ServerSide
{
    public class RemoteRequestProcessor : IRequestProcessor
    {
        private readonly IServerRequestCommunicator _serverRequestCommunicator;
        private readonly object _locker = new object();

        // ----- Constructors
        public RemoteRequestProcessor(IServerRequestCommunicator serverRequestCommunicator)
        {
            _serverRequestCommunicator = serverRequestCommunicator;
        }

        // ----- Public methods
        public Response Process(Request request)
        {
            lock (_locker) {
                _serverRequestCommunicator.SendToClient(request);
                return _serverRequestCommunicator.ReceiveFromClient<Response>();
            }
        }
        public void Close()
        {
            _serverRequestCommunicator.Close();
        }
    }

    public interface IServerRequestCommunicator
    {
        void SendToClient<TRequest>(TRequest request);
        TResponse ReceiveFromClient<TResponse>();
        void Close();
    }

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
                var jsonRequest = JsonConvert.SerializeObject(request);
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
                return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
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

    public class RequestException : Exception
    {
        public RequestException(string message, Exception exception) : base(message, exception)
        {
        }
        public RequestException(string message) : base(message)
        {
            
        }
    }
}