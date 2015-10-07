using ConnectUs.ClientSide.ModuleManagement;

namespace ConnectUs.ClientSide.Commands.LoadModule
{
    public class UnloadModuleCommand
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