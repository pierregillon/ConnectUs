using System;
using System.Collections.Generic;
using ConnectUs.ServerSide.Business;
using ConnectUs.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command.CommandLines.Module
{
    [CommandDescription(CommandName = "list-module", Description = "List the installed modules on remote client.", Category = "Module")]
    public class GetModuleList : CurrentClientCommand
    {
        public GetModuleList(Context context)
            : base(context)
        {
        }

        protected override string HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var moduleDecorator = new ModuleDecorator(remoteClient);
            var modules = moduleDecorator.GetIntalledModules();
            var results = new List<string>();
            foreach (var moduleState in modules) {
                results.Add(string.Format("{0}\t{1}\t{2}", moduleState.Name, moduleState.Version, moduleState.IsLoaded));
            }
            return string.Join(Environment.NewLine, results);
        }
    }
}