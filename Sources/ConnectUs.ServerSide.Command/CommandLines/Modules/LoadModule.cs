using System.Linq;
using ConnectUs.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Modules
{
    [CommandDescription(CommandName = "load-module", Description = "Load a module on remote client.")]
    public class LoadModule : CurrentClientCommand
    {
        public LoadModule(Context context) : base(context) {}

        protected override string HandleInternal(CommandLine commandLine, Client client)
        {
            var moduleName = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (moduleName == null) {
                return "You should define the module name.";
            }
            var moduleDecorator = new ModuleDecorator(client);
            moduleDecorator.LoadModule(moduleName.Value);
            return string.Empty;
        }
    }
}