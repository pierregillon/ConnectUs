namespace ConnectUs.Business
{
    public interface IRequestProcessor
    {
        Response Process(Request request);
    }
}