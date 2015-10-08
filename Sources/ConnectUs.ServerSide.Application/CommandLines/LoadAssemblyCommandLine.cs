using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConnectUs.ClientSide.Commands.Module;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public class LoadAssemblyCommandLine : ICommandLine
    {
        public string Name { get { return "install-module"; } }
        public string ExecuteCommand(RemoteClient remoteClient, IEnumerable<string> parameters)
        {
            var modulePath = Path.Combine(Directory.GetCurrentDirectory(), parameters.First());
            remoteClient.UploadFile(modulePath, "");
            var response = remoteClient.Send<AddModuleRequest, AddModuleResponse>(new AddModuleRequest
            {
                ModuleName = parameters.First()
            });
            return "done";
        }
    }
}