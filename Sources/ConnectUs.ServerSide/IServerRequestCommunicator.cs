namespace ConnectUs.ServerSide
{
    public interface IServerRequestCommunicator
    {
        void SendToClient<TRequest>(TRequest request);
        TResponse ReceiveFromClient<TResponse>();
        void Close();
    }
}