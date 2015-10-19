using System;
using System.Collections.Generic;

namespace ConnectUs.Core.ModuleManagement
{
    public interface IModuleManager
    {
        event EventHandler<ModuleLoadedEventArgs> ModuleLoaded;
        event EventHandler<ModuleUnloadedEventArgs> ModuleUnloaded;

        IEnumerable<Module> GetModules();
        Module GetModule(ModuleName moduleName);
        ModuleName AddModule(string modulePath);
        void RemoveModule(ModuleName modulePath);
        void LoadModule(ModuleName name);
        void UnloadModule(ModuleName name);
        IEnumerable<ModuleName> LoadModules();
    }
}