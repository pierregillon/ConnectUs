using System;
using System.Threading;
using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const string hostName = "localhost";
            const int port = 9000;

            while (true) {
                try {
                    Console.WriteLine("Trying to connect to the host '{0}' on port '{1}'", hostName, port);
                    var manualResetEvent = new ManualResetEvent(false);
                    var connection = TcpClientConnectionFactory.Build(hostName, port);
                    var continuous = new ContinuousRequestProcessor(connection, new RequestProcessor(new ClientInformationService()));
                    continuous.ConnectionLost += (sender, eventArgs) => manualResetEvent.Set();
                    Console.WriteLine("Client connecté");
                    continuous.StartProcessingRequestFromConnection();
                    manualResetEvent.WaitOne();
                }
                catch (ConnectionException) {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}