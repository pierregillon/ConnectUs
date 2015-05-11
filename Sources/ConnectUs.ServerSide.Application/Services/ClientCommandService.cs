using System;
using ConnectUs.ServerSide.Application.ViewModels;

namespace ConnectUs.ServerSide.Application.Services
{
    public class ClientCommandService : IClientCommandService
    {
        //private readonly List<ICommandExecutable> _commands = new List<ICommandExecutable>
        //{
        //    new GetInformationCommand(new ClientInformationService())
        //}; 

        public string ExecuteCommand(ClientViewModel clientViewModel, string[] arguments)
        {
            throw new NotImplementedException();
            //var commandName = arguments.First();
            //var parameters = arguments.Skip(1);
            //var command = _commands.First(x => x.Name == commandName);
            //var response = clientViewModel.Execute(command.BuildRequest(parameters));
            //return command.DisplayResponse(response);
        }
    }
}