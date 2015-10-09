namespace ConnectUs.ServerSide.Requests
{
    internal interface IRequestHandler
    {
        TResponse Execute<TRequest, TResponse>(TRequest request);
    }
}
