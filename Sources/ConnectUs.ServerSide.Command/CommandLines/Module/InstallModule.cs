using System.Linq;
using ConnectUs.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Module
{
    [CommandDescription(CommandName = "install-module", Description = "Install a module on remote client.", Category = "Module")]
    public class InstallModule : CurrentClientCommand
    {
        public InstallModule(Context context) : base(context) {}

        protected override string HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var loadModuleArgument = commandLine.Arguments.FirstOrDefault(x => x.Name == "load");
            var moduleName = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (moduleName == null) {
                return "You should define the module name.";
            }

            var moduleDecorator = new ModuleDecorator(remoteClient);
            moduleDecorator.UploadModule(moduleName.Value);
            moduleDecorator.AddModule(moduleName.Value);
            if (loadModuleArgument != null) {
                moduleDecorator.LoadModule(moduleName.Value);
            }
            return string.Empty;
        }
    }
}