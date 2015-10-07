using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "clients", Description = "Display the client list that are currently connected.")]
    internal class ShowClientList : ICommandLineHandler
    {
        private readonly Server _server;

        public ShowClientList(Server server)
        {
            _server = server;
        }

        public string Handle(CommandLine commandLine)
        {
            var elements = new List<string> { "Connected clients : " };
            for (var index = 0; index < _server.GetConnectedClients().ToList().Count; index++) {
                var client = _server.GetConnectedClients().ToList()[index];
                var information = client.GetClientInformation();
                elements.Add(string.Format("{0} {1} {2}", index+1, information.MachineName, information.Ip));
            }
            return string.Join(Environment.NewLine, elements.ToArray());
        }
    }
}