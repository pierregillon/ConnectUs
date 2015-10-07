using System.Linq;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "connect", Description = "Connect to a client.")]
    internal class ConnectClient : ICommandLineHandler
    {
        private readonly ClientList _clientList;
        private readonly Context _context;

        public ConnectClient(ClientList clientList, Context context)
        {
            _clientList = clientList;
            _context = context;
        }

        public string Handle(CommandLine commandLine)
        {
            var argument = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (argument == null) {
                return "You should define the client index.";
            }

            var index = int.Parse(argument.Value);
            _context.CurrentClient = _clientList.GetClients().ElementAt(index-1);
            return string.Empty;
        }
    }
}