using System;
using System.Collections.Generic;

namespace ConnectUs.ClientSide.Modules
{
    public interface IModuleManager
    {
        event EventHandler<ModuleAddedEventArgs> ModuleAdded;
        event EventHandler<ModuleRemovedEventArgs> ModuleRemoved;
        IEnumerable<Module> GetModules();
        void AddModule(string modulePath);
        void RemoveModule(string modulePath);
    }
}