﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConnectUs.ClientSide.Commands.LoadModule;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public class LoadAssemblyCommandLine : ICommandLine
    {
        public string Name { get { return "install-module"; } }
        public string ExecuteCommand(Client client, IEnumerable<string> parameters)
        {
            var modulePath = Path.Combine(Directory.GetCurrentDirectory(), parameters.First());
            client.Upload(modulePath, "");
            var response = client.ExecuteCommand<AddModuleRequest, AddModuleResponse>(new AddModuleRequest
            {
                ModuleName = parameters.First()
            });
            return "done";
        }
    }
}