using System.Linq;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "clients", Description = "Display the client list that are currently connected.")]
    internal class ShowClientList : CommandHandler, ICommandLineHandler
    {
        private readonly ClientList _clientList;

        public ShowClientList(ClientList clientList)
        {
            _clientList = clientList;
        }

        public void Handle(CommandLine commandLine)
        {
            var clients = _clientList.GetClients().ToList();
            for (var index = 0; index < clients.Count; index++) {
                var client = clients[index];
                WriteInfo(string.Format("{0} {1} {2} {3} {4} {5}",
                    (index + 1).ToString().PadRight(3),
                    client.MachineName.PadRight(15),
                    client.UserName.PadRight(15),
                    client.OperatingSystem.PadRight(15),
                    (client.Ip ?? string.Empty).PadRight(15),
                    (client.Latency + "ms").PadLeft(5)));
            }
        }
    }
}