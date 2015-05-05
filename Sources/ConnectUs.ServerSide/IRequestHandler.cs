namespace ConnectUs.ServerSide
{
    public interface IRequestHandler
    {
        TResponse Execute<TRequest, TResponse>(TRequest request);
    }
}
