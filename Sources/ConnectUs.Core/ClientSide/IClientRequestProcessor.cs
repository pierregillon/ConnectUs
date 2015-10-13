namespace ConnectUs.Core.ClientSide
{
    public interface IClientRequestProcessor
    {
        byte[] Process(byte[] data);
    }
}