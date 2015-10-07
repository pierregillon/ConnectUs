using System.IO;
using System.Linq;
using ConnectUs.ClientSide.Commands.LoadModule;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "install-module", Description = "Install a module on remote client.")]
    public class InstallModule : CurrentClientCommand
    {
        public InstallModule(Context context) : base(context)
        {
        }

        protected override string HandleInternal(CommandLine commandLine, Client client)
        {
            var moduleName = commandLine.Arguments.FirstOrDefault(x=>x.Name == "unknown");
            if (moduleName == null) {
                return "You should define the module name.";
            }

            var modulePath = Path.Combine(Directory.GetCurrentDirectory(), moduleName.Value);
            client.Upload(modulePath, "");
            client.ExecuteCommand<LoadModuleRequest, LoadModuleResponse>(new LoadModuleRequest
            {
                ModuleName = moduleName.Value
            });
            return "Ok";
        }
    }
}