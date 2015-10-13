using System.Linq;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Module
{
    [CommandDescription(CommandName = "install-module", Description = "Install a module on remote client.", Category = "Module")]
    public class InstallModule : CurrentClientCommand
    {
        public InstallModule(Context context) : base(context) {}

        protected override void HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var loadModuleArgument = commandLine.Arguments.FirstOrDefault(x => x.Name == "load");
            var moduleName = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (moduleName == null) {
                WriteWarning("You should define the module name.");
                return;
            }
            var moduleDecorator = new ModuleDecorator(remoteClient);
            moduleDecorator.UploadModule(moduleName.Value);
            moduleDecorator.AddModule(moduleName.Value);
            if (loadModuleArgument != null) {
                moduleDecorator.LoadModule(moduleName.Value);
            }
        }
    }
}