using System;
using ConnectUs.Business.Connections;
using ConnectUs.ServerSide.Command.CommandLines;
using SimpleInjector;

namespace ConnectUs.ServerSide.Command
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ConfigureIoc();
            var commandLineProcessor = container.GetInstance<CommandLineProcessor>();
            container.GetInstance<ClientList>();
            var context = container.GetInstance<Context>();

            while (true) {
                Console.Write("cus");
                if (context.CurrentClient != null) {
                    Console.Write(" | {0}", context.CurrentClient.MachineName);
                }
                Console.Write("> ");
                var command = Console.ReadLine();
                if (command == "exit") {
                    break;
                }
                var commandResult = commandLineProcessor.Execute(command);
                if (string.IsNullOrEmpty(commandResult) == false) {
                    Console.WriteLine(commandResult);
                    Console.WriteLine();
                }
            }
            Console.Write("< Press any key to exit >");
            Console.ReadKey();
        }

        private static Container ConfigureIoc()
        {
            var container = new Container();
            container.RegisterSingleton(() => new ServerConfiguration {Port = 9000});
            container.RegisterSingleton<IClientListener, ClientListener>();
            container.RegisterSingleton<IConnectionListener, TcpClientConnectionListener>();
            container.RegisterSingleton<CommandLineProcessor>();
            container.RegisterSingleton<ICommandLineHandlerLocator>(() => new CommandLineHandlerLocator(container));
            container.RegisterSingleton<Context>();
            container.RegisterSingleton<ClientList>();
            return container;
        }
    }
}