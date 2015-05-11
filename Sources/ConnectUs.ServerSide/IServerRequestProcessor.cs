namespace ConnectUs.ServerSide
{
    public interface IServerRequestProcessor
    {
        TResponse Process<TRequest, TResponse>(TRequest request);
        void Close();
    }
}