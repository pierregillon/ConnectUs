namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "stop", Description = "Stop the listen of new clients.")]
    internal class StopListeningClient : ICommandLineHandler
    {
        private readonly Server _server;

        public StopListeningClient(Server server)
        {
            _server = server;
        }

        public string Handle(CommandLine commandLine)
        {
            _server.Stop();
            return "Server stopped.";
        }
    }
}