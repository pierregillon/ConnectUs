namespace ConnectUs.ClientSide
{
    public interface IClientRequestProcessor
    {
        object Process(string requestName, string originalData);
    }
}