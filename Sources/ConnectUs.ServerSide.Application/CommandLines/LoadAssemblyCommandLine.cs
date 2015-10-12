using System.Collections.Generic;
using System.Linq;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.ServerSide.Business;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public class LoadAssemblyCommandLine : ICommandLine
    {
        public string Name { get { return "install-module"; } }
        public string ExecuteCommand(IRemoteClient remoteClient, IEnumerable<string> parameters)
        {
            var module = new ModuleDecorator(remoteClient);
            module.AddModule(parameters.First());
            return "done";
        }
    }
}