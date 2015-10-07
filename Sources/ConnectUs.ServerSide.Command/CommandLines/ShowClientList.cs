using System;
using System.Linq;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandLine(CommandName = "clients")]
    internal class ShowClientList : ICommandLineHandler
    {
        private readonly Server _server;

        public ShowClientList(Server server)
        {
            _server = server;
        }

        public string Handle(CommandLine commandLine)
        {
            var results = string.Empty;
            results += "Connected clients : " + Environment.NewLine;
            foreach (var client in _server.GetConnectedClients().ToList()) {
                var information= client.GetClientInformation();
                results += string.Format("{0} {1}", information.MachineName, information.Ip) + Environment.NewLine;
            }
            return results;
        }
    }
}