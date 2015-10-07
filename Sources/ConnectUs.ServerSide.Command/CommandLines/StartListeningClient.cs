using System.Linq;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "start", Description = "Start the listen of new clients.")]
    internal class StartListeningClient : ICommandLineHandler
    {
        private readonly IClientListener _clientListener;

        public StartListeningClient(IClientListener clientListener)
        {
            _clientListener = clientListener;
        }

        public string Handle(CommandLine commandLine)
        {
            var portArgument = commandLine.Arguments.FirstOrDefault(x => x.Name == "port");
            if (portArgument == null) {
                return "Port is missing.";
            }
            _clientListener.Start(int.Parse(portArgument.Value));
            return string.Format("Server started on port {0}.", portArgument.Value);
        }
    }
}