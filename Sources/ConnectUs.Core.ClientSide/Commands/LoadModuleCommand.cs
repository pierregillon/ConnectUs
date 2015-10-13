using ConnectUs.Core.ModuleManagement;
using ConnectUs.Modules.Integrated.ModuleManagement;

namespace ConnectUs.Core.ClientSide.Commands
{
    internal class LoadModuleCommand
    {
        private readonly IModuleManager _moduleManager;

        public LoadModuleCommand(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public LoadModuleResponse Execute(LoadModuleRequest request)
        {
            _moduleManager.LoadModule(new ModuleName(request.ModuleName));
            return new LoadModuleResponse();
        }
    }
}