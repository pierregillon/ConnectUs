﻿namespace ConnectUs.Common.LoadModule
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
            _moduleManager.InstallModule(@"Modules\ConnectUs.FileExplorer.dll");
            return new LoadModuleResponse();
        }
    }
}