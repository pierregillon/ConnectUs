namespace ConnectUs.Business
{
    public interface IConnection
    {
        TResponse Execute<TRequest, TResponse>(TRequest request);
    }
}