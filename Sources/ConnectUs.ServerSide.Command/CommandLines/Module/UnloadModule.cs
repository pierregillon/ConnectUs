﻿using System.Linq;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Module
{
    [CommandDescription(CommandName = "unload-module", Description = "Unload a module on remote client.", Category = "Module")]
    public class UnloadModule : CurrentClientCommand
    {
        public UnloadModule(Context context) : base(context) {}

        protected override string HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var moduleName = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (moduleName == null) {
                return "You should define the module name.";
            }
            var moduleDecorator = new ModuleDecorator(remoteClient);
            moduleDecorator.UnloadModule(moduleName.Value);
            return string.Empty;
        }
    }
}