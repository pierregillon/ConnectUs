namespace ConnectUs.ClientSide
{
    public interface IClientRequestProcessor
    {
        byte[] Process(string requestName, byte[] data);
    }
}