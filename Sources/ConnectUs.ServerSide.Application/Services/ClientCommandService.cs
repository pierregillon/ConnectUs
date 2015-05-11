using System.Collections.Generic;
using System.Linq;
using ConnectUs.Business;
using ConnectUs.Business.Commands;
using ConnectUs.ServerSide.Application.ViewModels;

namespace ConnectUs.ServerSide.Application.Services
{
    public class ClientCommandService : IClientCommandService
    {
        private readonly List<ICommandExecutable> _commands = new List<ICommandExecutable>
        {
            new GetInformationCommand(new ClientInformationService())
        }; 

        public string ExecuteCommand(ClientViewModel clientViewModel, string[] arguments)
        {
            var commandName = arguments.First();
            var parameters = arguments.Skip(1);
            var command = _commands.First(x => x.Name == commandName);
            var response = clientViewModel.Execute(command.BuildRequest(parameters));
            return command.DisplayResponse(response);
        }
    }
}