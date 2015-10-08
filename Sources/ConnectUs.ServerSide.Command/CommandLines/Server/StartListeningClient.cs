using System.Linq;

namespace ConnectUs.ServerSide.Command.CommandLines.Server
{
    [CommandDescription(CommandName = "start", Description = "Start the listen of new clients.")]
    internal class StartListeningClient : ICommandLineHandler
    {
        private readonly IClientListener _clientListener;
        private readonly int DefaultPort = 9000;

        public StartListeningClient(IClientListener clientListener)
        {
            _clientListener = clientListener;
        }

        public string Handle(CommandLine commandLine)
        {
            var port = DefaultPort;
            var portArgument = commandLine.Arguments.FirstOrDefault(x => x.Name == "port");
            if (portArgument != null) {
                port = int.Parse(portArgument.Value);
            }
            _clientListener.Start(port);
            return string.Format("Server started on port {0}.", port);
        }
    }
}