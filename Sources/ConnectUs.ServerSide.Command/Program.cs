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
            var server = new Server(new ServerConfiguration {Port = 9000});
            server.Start();
            while (true) {
                foreach (var connectedClient in server.GetConnectedClients().ToList()) {
                    try {
                        var clientInformation = connectedClient.GetClientInformation();
                        Console.WriteLine("client connecté : MachineName:{0}, Ip:{1}", clientInformation.MachineName, clientInformation.Ip);
                    }
                    catch (ConnectionException ex) {
                        Console.WriteLine(ex.Message);
                    }
                    connectedClient.CloseConnection();
                }
                Thread.Sleep(1000);
            }
        }
    }
}