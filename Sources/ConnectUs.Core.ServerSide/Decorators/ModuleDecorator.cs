﻿using System.Collections.Generic;
using System.IO;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Modules.Integrated.ModuleManagement;

namespace ConnectUs.Core.ServerSide.Decorators
{
    public class ModuleDecorator
    {
        private const string RemoteModuleDirectoryPath = "Modules";
        private readonly IRemoteClient _remoteClient;

        public ModuleDecorator(IRemoteClient remoteClient)
        {
            _remoteClient = remoteClient;
        }

        public void UploadModule(string moduleName)
        {
            var localModuleFilePath = Path.Combine(Directory.GetCurrentDirectory(), RemoteModuleDirectoryPath, moduleName + ".dll");
            _remoteClient.UploadFile(localModuleFilePath, RemoteModuleDirectoryPath);
        }
        public void AddModule(string moduleName)
        {
            var request = new AddModuleRequest(moduleName);
            _remoteClient.Send<AddModuleRequest, AddModuleResponse>(request);
        }
        public void LoadModule(string moduleName)
        {
            var request = new LoadModuleRequest(moduleName);
            _remoteClient.Send<LoadModuleRequest, LoadModuleResponse>(request);
        }
        public IEnumerable<ModuleState> GetIntalledModules()
        {
            var request = new ListModuleRequest();
            var response = _remoteClient.Send<ListModuleRequest, ListModuleResponse>(request);
            return response.Modules;
        }
        public void UnloadModule(string moduleName)
        {
            var request = new UnloadModuleRequest(moduleName);
            _remoteClient.Send<UnloadModuleRequest, UnloadModuleResponse>(request);
        }
    }
}