namespace ConnectUs.Core.ClientSide
{
    public interface IClientRequestProcessor
    {
        byte[] Process(string requestName, byte[] data);
    }
}