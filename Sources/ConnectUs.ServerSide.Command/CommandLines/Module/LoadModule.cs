using System.Linq;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Module
{
    [CommandDescription(CommandName = "load-module", Description = "Load a module on remote client.", Category = "Module")]
    public class LoadModule : CurrentClientCommand
    {
        public LoadModule(Context context) : base(context) {}

        protected override void HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var moduleName = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (moduleName == null) {
                WriteWarning("You should define the module name.");
            }
            else {
                var moduleDecorator = new ModuleDecorator(remoteClient);
                moduleDecorator.LoadModule(moduleName.Value);
            }
        }
    }
}