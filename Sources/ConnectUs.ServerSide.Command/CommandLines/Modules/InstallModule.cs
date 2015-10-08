using System.Linq;
using ConnectUs.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Modules
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

            var moduleDecorator = new ModuleDecorator(client);
            moduleDecorator.UploadModule(moduleName.Value);
            moduleDecorator.AddModule(moduleName.Value);
            if (loadModuleArgument != null) {
                moduleDecorator.LoadModule(moduleName.Value);
            }
            return string.Empty;
        }
    }
}