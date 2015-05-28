using System.IO;
using ConnectUs.ClientSide.Commands.Upload;

namespace ConnectUs.ServerSide
{
    public class RemoteRequestProcessor : IServerRequestProcessor
    {
        private readonly IServerRequestCommunicator _serverRequestCommunicator;
        private readonly IServerFileCommunicator _serverFileCommunicator;
        private readonly object _locker = new object();

        // ----- Constructors
        public RemoteRequestProcessor(IServerRequestCommunicator serverRequestCommunicator, IServerFileCommunicator serverFileCommunicator)
        {
            _serverRequestCommunicator = serverRequestCommunicator;
            _serverFileCommunicator = serverFileCommunicator;
        }

        // ----- Public methods
        public TResponse Process<TRequest, TResponse>(TRequest request)
        {
            lock (_locker) {
                _serverRequestCommunicator.SendToClient(request);
                return _serverRequestCommunicator.ReceiveFromClient<TResponse>();
            }
        }
        public void Upload(string filePath)
        {
            lock (_locker) {
                _serverRequestCommunicator.SendToClient(new UploadRequest{FileName=Path.GetFileName(filePath)});
                _serverFileCommunicator.Upload(filePath);
                _serverRequestCommunicator.ReceiveFromClient<UploadResponse>();
            }
        }
        public void Close()
        {
            _serverRequestCommunicator.Close();
        }
    }
}