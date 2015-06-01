using System;
using System.IO;
using ConnectUs.ClientSide.Commands.Upload;

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
        public TResponse ProcessRequest<TRequest, TResponse>(TRequest request)
        {
            lock (_locker) {
                _serverRequestCommunicator.SendRequest(request);
                return _serverRequestCommunicator.ReceiveResponse<TResponse>();
            }
        }
        public string UploadFile(string sourceFilePath, string targetDirectory)
        {
            var fileName = Path.GetFileName(sourceFilePath);
            if (fileName == null) {
                throw new Exception(string.Format("The file '{0}' is not a valid file.", sourceFilePath));
            }
            lock (_locker) {
                _serverRequestCommunicator.SendRequest(new UploadRequest { FilePath = Path.Combine(targetDirectory, fileName) });
                _serverRequestCommunicator.SendFile(sourceFilePath);
                var response =_serverRequestCommunicator.ReceiveResponse<UploadResponse>();
                return response.FilePath;
            }
        }
        public void Close()
        {
            _serverRequestCommunicator.Close();
        }
    }
}