using System.Net.Sockets;
using ConnectUs.Business.Connections;
using ConnectUs.Business.Encodings;

namespace ConnectUs.ClientSide
{
    public class Client
    {
        public void Start(string hostName, int port)
        {
            var client = GetTcpClient(hostName, port);
            var continuous = new ContinuousRequestProcessor(new TcpClientConnection(client, new JsonEncoder()), new RequestProcessor(new ClientInformationService()));
            continuous.StartProcessingRequestFromConnection();
        }

        private static TcpClient GetTcpClient(string hostName, int port)
        {
            try {
                var client = new TcpClient();
                client.Connect(hostName, port);
                return client;
            }
            catch (SocketException ex) {
                throw new ClientException();
            }
        }
    }
}