using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "clients", Description = "Display the client list that are currently connected.")]
    internal class ShowClientList : ICommandLineHandler
    {
        private readonly ClientList _clientList;

        public ShowClientList(ClientList clientList)
        {
            _clientList = clientList;
        }

        public string Handle(CommandLine commandLine)
        {
            var elements = new List<string> { "Connected clients : " };
            var clients = _clientList.GetClients().ToList();
            for (var index = 0; index < clients.Count; index++)
            {
                var client = clients[index];
                elements.Add(string.Format("{0}\t{1}\t{2}\t{3}ms", index + 1, client.MachineName, client.Ip, client.Latency));
            }
            return string.Join(Environment.NewLine, elements.ToArray());
        }
    }
}