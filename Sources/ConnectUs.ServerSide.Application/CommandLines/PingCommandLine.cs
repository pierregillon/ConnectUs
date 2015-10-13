using System.Collections.Generic;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public class PingCommandLine : ICommandLine
    {
        public string Name
        {
            get { return "ping"; }
        }

        public string ExecuteCommand(IRemoteClient remoteClient, IEnumerable<string> parameters)
        {
            var decorator = new ClientInformationDecorator(remoteClient);
            var duration = decorator.Ping();
            return string.Format("Ping to client : {0} ms", duration);
        }
    }
}