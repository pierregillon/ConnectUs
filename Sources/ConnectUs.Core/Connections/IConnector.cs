namespace ConnectUs.Core.Connections
{
    public interface IConnector
    {
        IConnection Connect(string host, int port);
    }
}