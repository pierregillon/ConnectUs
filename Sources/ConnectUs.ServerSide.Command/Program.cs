using System;
using ConnectUs.ServerSide.Command.CommandLines;
using SimpleInjector;

namespace ConnectUs.ServerSide.Command
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new Container();
            container.RegisterSingleton(() => new ServerConfiguration {Port = 9000});
            container.RegisterSingleton<Server>();
            container.RegisterSingleton<CommandLineProcessor>();
            container.RegisterSingleton<ICommandLineHandlerLocator>(() => new CommandLineHandlerLocator(container));
            container.Register<ShowClientList>();
            container.RegisterSingleton<Context>();

            var commandLineProcessor = container.GetInstance<CommandLineProcessor>();
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
            Console.Write("< Press any key to exit >");
            Console.ReadKey();
        }
    }
}