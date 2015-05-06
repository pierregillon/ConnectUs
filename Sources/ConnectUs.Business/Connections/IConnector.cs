namespace ConnectUs.Business.Connections
{
    public interface IConnector
    {
        IConnection Connect(string host, int port);
    }
}