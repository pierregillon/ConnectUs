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
            _moduleManager.LoadModule(new ModuleName(request.ModuleName));
            return new LoadModuleResponse();
        }
    }
}