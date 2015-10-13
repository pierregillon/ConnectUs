using ConnectUs.Core.ModuleManagement;
using ConnectUs.Modules.Integrated.ModuleManagement;

namespace ConnectUs.Core.ClientSide.Commands
{
    internal class UnloadModuleCommand
    {
        private readonly IModuleManager _moduleManager;

        public UnloadModuleCommand(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public UnloadModuleResponse Execute(UnloadModuleRequest request)
        {
            _moduleManager.UnloadModule(new ModuleName(request.ModuleName));
            return new UnloadModuleResponse();
        }
    }
}