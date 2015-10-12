using System.Linq;
using ConnectUs.ClientSide.ModuleManagement;
using ConnectUs.Modules.Integrated.ModuleManagement;

namespace ConnectUs.ClientSide.Commands.Module
{
    internal class ListModuleCommand
    {
        private readonly IModuleManager _moduleManager;
        private const string ModuleDirectoryPath = "modules";

        public ListModuleCommand(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public ListModuleResponse Execute(ListModuleRequest request)
        {
            return new ListModuleResponse
            {
                Modules = _moduleManager.GetModules().Select(x => new ModuleState
                {
                    Name = x.Name.ToString(),
                    Version = x.Version.ToString(),
                    IsLoaded = x.IsLoaded
                })
            };
        }
    }
}