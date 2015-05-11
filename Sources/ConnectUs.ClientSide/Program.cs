using System;
using System.Threading;

namespace ConnectUs.ClientSide
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const string hostName = "localhost";
            const int port = 9000;

            var client = new Client();
            while (true) {
                try {
                    Console.WriteLine("Trying to connect to the host '{0}' on port '{1}'", hostName, port);
                    client.Start(hostName, port);
                }
                catch (ClientException) {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}