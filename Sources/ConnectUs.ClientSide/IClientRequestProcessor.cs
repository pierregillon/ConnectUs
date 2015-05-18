namespace ConnectUs.ClientSide
{
    public interface IClientRequestProcessor
    {
        string Process(string requestName, string originalData);
    }
}