namespace ConnectUs.ServerSide
{
    public interface IServerRequestCommunicator
    {
        void SendRequest<TRequest>(TRequest request);
        TResponse ReceiveResponse<TResponse>();
        void SendFile(string filePath);
        void Close();
    }
}