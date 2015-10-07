using System;

namespace ConnectUs.ServerSide.Command
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var server = new Server(new ServerConfiguration {Port = 9000});
            var commandLineProcessor = new CommandLineProcessor(new CommandLineHandlerLocator());
            server.Start();
            while (true) {
                Console.Write("cus> ");
                var command = Console.ReadLine();
                if (command == "exit") {
                    break;
                }
                var commandResult = commandLineProcessor.Execute(command);
                Console.WriteLine(commandResult);
                Console.WriteLine();
            }
            server.Stop();
            foreach (var connectedClient in server.GetConnectedClients()) {
                connectedClient.CloseConnection();
            }
            Console.Write("< Press any key to exit >");
            Console.ReadKey();
        }
    }
}