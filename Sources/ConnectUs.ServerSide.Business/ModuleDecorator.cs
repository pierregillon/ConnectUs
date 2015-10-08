﻿using System.Collections.Generic;
using System.IO;
using ConnectUs.ClientSide.Commands.Module;

namespace ConnectUs.ServerSide.Decorators
{
    public class ModuleDecorator
    {
        private const string RemoteModuleDirectoryPath = "Modules";
        private readonly RemoteClient _remoteClient;

        public ModuleDecorator(RemoteClient remoteClient)
        {
            _remoteClient = remoteClient;
        }

        public void UploadModule(string moduleName)
        {
            var localModuleFilePath = Path.Combine(Directory.GetCurrentDirectory(), RemoteModuleDirectoryPath, moduleName + ".dll");
            _remoteClient.Upload(localModuleFilePath, RemoteModuleDirectoryPath);
        }
        public void AddModule(string moduleName)
        {
            var request = new AddModuleRequest(moduleName);
            _remoteClient.ExecuteCommand<AddModuleRequest, AddModuleResponse>(request);
        }
        public void LoadModule(string moduleName)
        {
            var request = new LoadModuleRequest(moduleName);
            _remoteClient.ExecuteCommand<LoadModuleRequest, LoadModuleResponse>(request);
        }
        public IEnumerable<ModuleState> GetIntalledModules()
        {
            var request = new ListModuleRequest();
            var response = _remoteClient.ExecuteCommand<ListModuleRequest, ListModuleResponse>(request);
            return response.Modules;
        }
        public void UnloadModule(string moduleName)
        {
            var request = new UnloadModuleRequest(moduleName);
            _remoteClient.ExecuteCommand<UnloadModuleRequest, UnloadModuleResponse>(request);
        }
    }
}