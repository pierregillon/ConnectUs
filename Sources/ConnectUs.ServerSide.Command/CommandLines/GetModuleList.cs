using System;
using System.Collections.Generic;
using ConnectUs.ClientSide.Commands.LoadModule;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "list-module", Description = "List all the installed module on remote client.")]
    public class GetModuleList : CurrentClientCommand
    {
        public GetModuleList(Context context)
            : base(context) {}

        protected override string HandleInternal(CommandLine commandLine, Client client)
        {
            var response = client.ExecuteCommand<ListModuleRequest, ListModuleResponse>(new ListModuleRequest());
            var results = new List<string>
            {
                "Modules on client side :"
            };
            foreach (var moduleState in response.Modules) {
                results.Add(string.Format("{0}\t{1}\t{2}", moduleState.Name, moduleState.Version, moduleState.IsLoaded));
            }
            return string.Join(Environment.NewLine, results);
        }
    }
}