using System;
using System.Collections.Generic;
using ConnectUs.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Modules
{
    [CommandDescription(CommandName = "list-module", Description = "List all the installed module on remote client.")]
    public class GetModuleList : CurrentClientCommand
    {
        public GetModuleList(Context context)
            : base(context)
        {
        }

        protected override string HandleInternal(CommandLine commandLine, Client client)
        {
            var moduleDecorator = new ModuleDecorator(client);
            var modules = moduleDecorator.GetIntalledModules();
            var results = new List<string>();
            foreach (var moduleState in modules) {
                results.Add(string.Format("{0}\t{1}\t{2}", moduleState.Name, moduleState.Version, moduleState.IsLoaded));
            }
            return string.Join(Environment.NewLine, results);
        }
    }
}