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
        public void UploadFile(string filePath)
        {
            lock (_locker) {
                _serverRequestCommunicator.SendRequest(new UploadRequest{FileName=Path.GetFileName(filePath)});
                _serverRequestCommunicator.SendFile(filePath);
                _serverRequestCommunicator.ReceiveResponse<UploadResponse>();
            }
        }
        public void Close()
        {
            _serverRequestCommunicator.Close();
        }
    }
}