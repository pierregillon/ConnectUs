namespace ConnectUs.ServerSide
{
    public interface IClient
    {
        ClientInfo ClientInfo { get; }
        event ClientDisconnectedEventHandler ClientDisconnected;
        TResponse ExecuteRequest<TRequest, TResponse>(TRequest request);
    }
}
