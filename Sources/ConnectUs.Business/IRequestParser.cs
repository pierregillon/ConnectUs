namespace ConnectUs.Business
{
    public interface IRequestParser
    {
        string GetRequestName(string data);
        string GetError(string data);
    }
}