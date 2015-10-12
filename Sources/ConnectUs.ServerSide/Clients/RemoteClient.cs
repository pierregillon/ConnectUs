using System;
using System.IO;
using ConnectUs.Modules.Integrated.FileTransfert;
using ConnectUs.ServerSide.Requests;

namespace ConnectUs.ServerSide.Clients
{
    internal class RemoteClient : IRemoteClient
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
                try {
                    _requestDispatcher.SendRequest(request);
                    return _requestDispatcher.ReceiveResponse<TResponse>();
                }
                catch (RequestException ex) {
                    throw new RemoteClientException(string.Format("An error occured during the execution of the request {0}.", typeof (TRequest).Name), ex);
                }
            }
        }
        public string UploadFile(string sourceFilePath, string targetDirectory)
        {
            var fileName = Path.GetFileName(sourceFilePath);
            if (fileName == null) {
                throw new Exception(string.Format("The file '{0}' is not a valid file.", sourceFilePath));
            }
            lock (_locker) {
                try {
                    _requestDispatcher.SendRequest(new UploadRequest {FilePath = Path.Combine(targetDirectory, fileName)});
                    _requestDispatcher.SendFile(sourceFilePath);
                    var response = _requestDispatcher.ReceiveResponse<UploadResponse>();
                    return response.FilePath;
                }
                catch (RequestException ex) {
                    throw new RemoteClientException(string.Format("An error occured during the upload of {0}.", Path.GetFileName(sourceFilePath)), ex);
                }
            }
        }
        public string DownloadFile(string remoteFilePath, string localFolder)
        {
            var remoteFileName = Path.GetFileName(remoteFilePath);
            if (string.IsNullOrEmpty(remoteFileName)) {
                throw new Exception("The remote file path is not a file.");
            }
            if (Directory.Exists(localFolder) == false) {
                Directory.CreateDirectory(localFolder);
            }
            var localFilePath = Path.Combine(localFolder, remoteFileName);
            lock (_locker) {
                try {
                    _requestDispatcher.SendRequest(new DownloadRequest { FilePath = remoteFilePath });
                    _requestDispatcher.ReceiveFile(localFilePath);
                    var response = _requestDispatcher.ReceiveResponse<DownloadResponse>();
                    return response.RemoteFilePath;
                }
                catch (RequestException ex) {
                    if (File.Exists(localFilePath)) {
                        File.Delete(localFilePath);
                    }
                    throw new RemoteClientException(string.Format("An error occured during the download of {0}.", Path.GetFileName(remoteFilePath)), ex);
                }
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