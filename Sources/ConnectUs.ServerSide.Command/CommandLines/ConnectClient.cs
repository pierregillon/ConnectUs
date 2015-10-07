using System.Linq;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "connect", Description = "Connect to a client.")]
    internal class ConnectClient : ICommandLineHandler
    {
        private readonly Server _server;
        private readonly Context _context;

        public ConnectClient(Server server, Context context)
        {
            _server = server;
            _context = context;
        }

        public string Handle(CommandLine commandLine)
        {
            var argument = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (argument == null) {
                return "You should define the client index.";
            }

            var index = int.Parse(argument.Value);
            _context.CurrentClient = _server.GetConnectedClients().ElementAt(index-1);
            return "Ok";
        }
    }
}