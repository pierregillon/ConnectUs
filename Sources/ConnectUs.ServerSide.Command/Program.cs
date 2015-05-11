using System;
using System.Linq;
using System.Threading;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide.Command
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var server = new Server(new ClientListener(new TcpClientConnectionListener(), new ServerConfiguration {Port = 9000}));
            server.Start();
            while (server.GetConnectedClients().Any() == false) {
                Thread.Sleep(1000);
            }
            var client = server.GetConnectedClients().First();
            Console.WriteLine("client connecté");
            Console.ReadLine();
            var clientInformation = client.GetClientInformation();
            Console.WriteLine("MachineName:{0}, Ip:{1}", clientInformation.MachineName, clientInformation.Ip);
            Console.ReadLine();
        }
    }
}