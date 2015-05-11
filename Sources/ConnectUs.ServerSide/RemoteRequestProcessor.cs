namespace ConnectUs.ServerSide
{
    public class RemoteRequestProcessor : IServerRequestProcessor
    {
        private readonly IServerRequestCommunicator _serverRequestCommunicator;
        private readonly object _locker = new object();

        // ----- Constructors
        public RemoteRequestProcessor(IServerRequestCommunicator serverRequestCommunicator)
        {
            _serverRequestCommunicator = serverRequestCommunicator;
        }

        // ----- Public methods
        public TResponse Process<TRequest, TResponse>(TRequest request)
        {
            lock (_locker) {
                _serverRequestCommunicator.SendToClient(request);
                return _serverRequestCommunicator.ReceiveFromClient<TResponse>();
            }
        }
        public void Close()
        {
            _serverRequestCommunicator.Close();
        }
    }
}