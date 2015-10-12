namespace ConnectUs.Core.ServerSide.Requests
{
    public interface IRequestDispatcher
    {
        void SendRequest<TRequest>(TRequest request);
        TResponse ReceiveResponse<TResponse>();
        void SendFile(string filePath);
        void ReceiveFile(string filePath);
        void Close();
    }
}