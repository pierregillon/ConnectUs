using System.Collections.Generic;
using System.IO;
using ConnectUs.ClientSide.Commands.LoadModule;

namespace ConnectUs.ServerSide.Decorators
{
    public class ModuleDecorator
    {
        private const string RemoteModuleDirectoryPath = "Modules";
        private readonly Client _client;

        public ModuleDecorator(Client client)
        {
            _client = client;
        }

        public void UploadModule(string moduleName)
        {
            var localModuleFilePath = Path.Combine(Directory.GetCurrentDirectory(), RemoteModuleDirectoryPath, moduleName + ".dll");
            _client.Upload(localModuleFilePath, RemoteModuleDirectoryPath);
        }
        public void AddModule(string moduleName)
        {
            var request = new AddModuleRequest(moduleName);
            _client.ExecuteCommand<AddModuleRequest, AddModuleResponse>(request);
        }
        public void LoadModule(string moduleName)
        {
            var request = new LoadModuleRequest(moduleName);
            _client.ExecuteCommand<LoadModuleRequest, LoadModuleResponse>(request);
        }
        public IEnumerable<ModuleState> GetIntalledModules()
        {
            var request = new ListModuleRequest();
            var response = _client.ExecuteCommand<ListModuleRequest, ListModuleResponse>(request);
            return response.Modules;
        }
        public void UnloadModule(string moduleName)
        {
            var request = new UnloadModuleRequest(moduleName);
            _client.ExecuteCommand<UnloadModuleRequest, UnloadModuleResponse>(request);
        }
    }
}