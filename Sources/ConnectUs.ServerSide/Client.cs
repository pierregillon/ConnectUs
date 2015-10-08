namespace ConnectUs.ServerSide
{
    public class Client
    {
        private readonly IServerRequestProcessor _serverRequestProcessor;

        public Client(IServerRequestProcessor serverRequestProcessor)
        {
            _serverRequestProcessor = serverRequestProcessor;
        }

        public TResponse ExecuteCommand<TRequest, TResponse>(TRequest request)
        {
            return _serverRequestProcessor.ProcessRequest<TRequest, TResponse>(request);
        }
        public string Upload(string sourceFilePath, string targetDirectory)
        {
            return _serverRequestProcessor.UploadFile(sourceFilePath, targetDirectory);
        }
        public void CloseConnection()
        {
            _serverRequestProcessor.Close();
        }
    }
}