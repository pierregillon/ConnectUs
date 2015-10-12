using System.Net.Sockets;

namespace ConnectUs.Core.Connections
{
    public class TcpClientConnectionFactory
    {
        public static IConnection Build(string hostName, int port, int timeout = 0)
        {
            try {
                var client = new TcpClient();
                client.Connect(hostName, port);
                var connection = new TcpClientConnection(client, timeout);
                return connection;
            }
            catch (SocketException ex) {
                throw new ConnectionException(string.Format("Unable to create a connection to the host '{0}' on the port '{1}'.", hostName, port), ex);
            }
        }
    }
}