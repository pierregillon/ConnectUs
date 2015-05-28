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
            const string filePath = @"Modules\ConnectUs.FileExplorer.dll";
            var name = _moduleManager.AddModule(filePath);
            _moduleManager.LoadModule(name);
            return new LoadModuleResponse();
        }
    }
}