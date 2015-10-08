using System;
using System.IO;
using ConnectUs.ClientSide.Commands.Upload;

namespace ConnectUs.ServerSide
{
    public class RemoteClient : IRemoteClient
    {
        private readonly IServerRequestCommunicator _serverRequestCommunicator;
        private readonly object _locker = new object();

        // ----- Constructors
        public RemoteClient(IServerRequestCommunicator serverRequestCommunicator)
        {
            _serverRequestCommunicator = serverRequestCommunicator;
        }

        // ----- Public methods
        public TResponse ExecuteCommand<TRequest, TResponse>(TRequest request)
        {
            lock (_locker) {
                _serverRequestCommunicator.SendRequest(request);
                return _serverRequestCommunicator.ReceiveResponse<TResponse>();
            }
        }
        public string Upload(string sourceFilePath, string targetDirectory)
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
        public void CloseConnection()
        {
            lock (_locker) {
                _serverRequestCommunicator.Close();
            }
        }
    }
}