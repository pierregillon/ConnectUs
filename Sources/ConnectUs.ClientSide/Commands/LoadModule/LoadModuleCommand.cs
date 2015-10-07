﻿using System.IO;
using ConnectUs.ClientSide.ModuleManagement;

namespace ConnectUs.ClientSide.Commands.LoadModule
{
    public class LoadModuleCommand
    {
        private readonly IModuleManager _moduleManager;

        public LoadModuleCommand(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public LoadModuleResponse Execute(LoadModuleRequest request)
        {
            var filePath = Path.Combine(@"c:\TEMP\", request.ModuleName);
            var name = _moduleManager.AddModule(filePath);
            _moduleManager.LoadModule(name);
            return new LoadModuleResponse();
        }
    }
}