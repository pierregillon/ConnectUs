using System.Collections.Generic;
using ConnectUs.ClientSide.Commands.LoadModule;

namespace ConnectUs.ServerSide.CommandLines
{
    public class LoadAssemblyCommandLine : ICommandLine
    {
        public string Name { get { return "test"; } }
        public string ExecuteCommand(Client client, IEnumerable<string> parameters)
        {
            client.ExecuteCommand<LoadModuleRequest, LoadModuleResponse>(new LoadModuleRequest());
            return "done";
        }
    }
}