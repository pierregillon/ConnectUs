using System.Collections.Generic;
using ConnectUs.Business.Commands;
using ConnectUs.Business.Commands.ClientInformation;

namespace ConnectUs.ClientSide
{
    public class ModuleService : IModuleService
    {
        private readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>
        {
            {typeof(GetClientInformationRequest).Name, new GetInformationCommand()},
            {typeof(PingRequest).Name, new PingCommand()},
        }; 

        public ICommand GetCommand(string requestName)
        {
            ICommand command;
            _commands.TryGetValue(requestName, out command);
            return command;
        }
    }
}