namespace ConnectUs.ServerSide.Command.CommandLines.Server
{
    [CommandDescription(CommandName = "stop", Description = "Stop the listen of new clients.", Category = "Server")]
    internal class StopListeningClient : ICommandLineHandler
    {
        private readonly IClientListener _clientListener;

        public StopListeningClient(IClientListener clientListener)
        {
            _clientListener = clientListener;
        }

        public string Handle(CommandLine commandLine)
        {
            _clientListener.Stop();
            return "Server stopped.";
        }
    }
}