﻿using System.IO;
using System.Linq;
using ConnectUs.ClientSide.Commands.LoadModule;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "install-module", Description = "Install a module on remote client.")]
    public class InstallModule : CurrentClientCommand
    {
        public InstallModule(Context context) : base(context) {}

        protected override string HandleInternal(CommandLine commandLine, Client client)
        {
            var loadModuleArgument = commandLine.Arguments.FirstOrDefault(x => x.Name == "load");
            var moduleName = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (moduleName == null) {
                return "You should define the module name.";
            }

            var localModuleFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Modules", moduleName.Value + ".dll");
            var remoteModuleDirectoryPath = "Modules";
            client.Upload(localModuleFilePath, remoteModuleDirectoryPath);
            client.ExecuteCommand<AddModuleRequest, AddModuleResponse>(new AddModuleRequest
            {
                ModuleName = moduleName.Value,
            });
            if (loadModuleArgument != null) {
                client.ExecuteCommand<LoadModuleRequest, LoadModuleResponse>(new LoadModuleRequest
                {
                    ModuleName = moduleName.Value,
                });
            }
            return "Ok";
        }
    }
}