namespace ConnectUs.Business
{
    public interface IConnector
    {
        IConnection Connect(string host, int port);
    }
}