namespace ConnectUs.Core.ClientSide
{
    internal class RemoteServer
    {
        public string Host { get; private set; }
        public int Port { get; private set; }
        
        public RemoteServer(string host, int port)
        {
            Host = host;
            Port = port;
        }
    }
}