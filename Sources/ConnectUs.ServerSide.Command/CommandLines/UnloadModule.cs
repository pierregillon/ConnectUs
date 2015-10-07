using System.Linq;
using ConnectUs.ClientSide.Commands.LoadModule;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "unload-module", Description = "Unload a module on remote client.")]
    public class UnloadModule : CurrentClientCommand
    {
        public UnloadModule(Context context) : base(context) {}

        protected override string HandleInternal(CommandLine commandLine, Client client)
        {
            var moduleName = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (moduleName == null) {
                return "You should define the module name.";
            }
            client.ExecuteCommand<UnloadModuleRequest, UnloadModuleResponse>(new UnloadModuleRequest
            {
                ModuleName = moduleName.Value
            });
            return "Ok";
        }
    }
}