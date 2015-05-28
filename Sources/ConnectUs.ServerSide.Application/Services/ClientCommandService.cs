using System;
using System.Collections.Generic;
using System.Linq;
using ConnectUs.ServerSide.Application.ViewModels;
using ConnectUs.ServerSide.CommandLines;

namespace ConnectUs.ServerSide.Application.Services
{
    public class ClientCommandService : IClientCommandService
    {
        private readonly List<ICommandLine> _commandLines = new List<ICommandLine>
        {
            new PingCommandLine(),
            new ExploreDirectoryCommandLine(),
            new LoadAssemblyCommandLine(),
            new UploadCommandLine()
        };

        public string ExecuteCommand(ClientViewModel clientViewModel, string[] arguments)
        {
            try {
                var commandName = arguments.First();
                var command = _commandLines.FirstOrDefault(x => string.Equals(x.Name, commandName, StringComparison.CurrentCultureIgnoreCase));
                if (command == null) {
                    return "Unknown command.";
                }
                else {
                    var parameters = Enumerable.Empty<string>();
                    if (arguments.Length > 1) {
                        parameters = arguments.Skip(1);
                    }
                    return clientViewModel.ExecuteCommand(command, parameters);
                }
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }
    }
}