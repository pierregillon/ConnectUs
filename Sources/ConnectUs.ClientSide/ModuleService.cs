using System.Collections.Generic;
using ConnectUs.Business.Commands.ClientInformation;
using ConnectUs.FileExplorer;

namespace ConnectUs.ClientSide
{
    public class ModuleService : IModuleService
    {
        private readonly Dictionary<string, object> _commands = new Dictionary<string, object>
        {
            {typeof (GetClientInformationRequest).Name, new GetInformationCommand()},
            {typeof (PingRequest).Name, new PingCommand()},
            {typeof (ExploreDirectoryRequest).Name, new ExploreDirectoryCommand()},
        };

        public object GetCommand(string requestName)
        {
            object command;
            _commands.TryGetValue(requestName, out command);
            return command;
        }
    }
}