using System;
using System.IO;
using ConnectUs.ClientSide.Commands.Upload;

namespace ConnectUs.ServerSide
{
    public class RemoteClient : IRemoteClient
    {
        private readonly IRequestDispatcher _requestDispatcher;
        private readonly object _locker = new object();

        // ----- Constructors
        public RemoteClient(IRequestDispatcher requestDispatcher)
        {
            _requestDispatcher = requestDispatcher;
        }

        // ----- Public methods
        public TResponse Send<TRequest, TResponse>(TRequest request)
        {
            lock (_locker) {
                _requestDispatcher.SendRequest(request);
                return _requestDispatcher.ReceiveResponse<TResponse>();
            }
        }
        public string UploadFile(string sourceFilePath, string targetDirectory)
        {
            var fileName = Path.GetFileName(sourceFilePath);
            if (fileName == null) {
                throw new Exception(string.Format("The file '{0}' is not a valid file.", sourceFilePath));
            }
            lock (_locker) {
                _requestDispatcher.SendRequest(new UploadRequest { FilePath = Path.Combine(targetDirectory, fileName) });
                _requestDispatcher.SendFile(sourceFilePath);
                var response =_requestDispatcher.ReceiveResponse<UploadResponse>();
                return response.FilePath;
            }
        }
        public void Close()
        {
            lock (_locker) {
                _requestDispatcher.Close();
            }
        }
    }
}