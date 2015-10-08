namespace ConnectUs.ServerSide
{
    public interface IRequestDispatcher
    {
        void SendRequest<TRequest>(TRequest request);
        TResponse ReceiveResponse<TResponse>();
        void SendFile(string filePath);
        void Close();
    }
}