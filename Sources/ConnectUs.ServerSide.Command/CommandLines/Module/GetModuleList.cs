using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Module
{
    [CommandDescription(CommandName = "list-modules", Description = "List the installed modules on remote client.", Category = "Module")]
    public class GetModuleList : CurrentClientCommand
    {
        public GetModuleList(Context context)
            : base(context)
        {
        }

        protected override void HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var moduleDecorator = new ModuleDecorator(remoteClient);
            var modules = moduleDecorator.GetIntalledModules();
            foreach (var moduleState in modules) {
                WriteInfo("{0} {1} {2}", 
                    moduleState.Name.PadLeft(15),
                    moduleState.Version.PadLeft(15),
                    moduleState.IsLoaded.ToString().PadLeft(15));
            }
        }
    }
}