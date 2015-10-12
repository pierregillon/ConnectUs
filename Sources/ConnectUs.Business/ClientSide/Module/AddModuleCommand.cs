using System;
using System.IO;
using ConnectUs.ClientSide.ModuleManagement;
using ConnectUs.Modules.Integrated.ModuleManagement;

namespace ConnectUs.ClientSide.Commands.Module
{
    internal class AddModuleCommand
    {
        private readonly IModuleManager _moduleManager;
        private const string ModuleDirectoryPath = "modules";

        public AddModuleCommand(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public AddModuleResponse Execute(AddModuleRequest request)
        {
            var existingModule = _moduleManager.GetModule(new ModuleName(request.ModuleName));
            if (existingModule != null) {
                throw new Exception(string.Format("The module '{0}' already exist on client.", request.ModuleName));
            }
            var filePath = Path.Combine(ModuleDirectoryPath, request.ModuleName + ".dll");
            _moduleManager.AddModule(filePath);
            return new AddModuleResponse();
        }
    }
}