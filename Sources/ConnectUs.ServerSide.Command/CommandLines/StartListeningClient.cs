namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandLine(CommandName = "start")]
    internal class StartListeningClient : ICommandLineHandler
    {
        private readonly Server _server;

        public StartListeningClient(Server server)
        {
            _server = server;
        }

        public string Handle(CommandLine commandLine)
        {
            _server.Start();
            return "Server started.";
        }
    }
}