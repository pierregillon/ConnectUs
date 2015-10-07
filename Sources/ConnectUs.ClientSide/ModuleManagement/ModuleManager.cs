using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConnectUs.ClientSide.ModuleManagement
{
    public class ModuleManager : IModuleManager
    {
        private readonly List<Module> _modules = new List<Module>();

        public event EventHandler<ModuleLoadedEventArgs> ModuleLoaded;
        protected virtual void OnModuleLoaded(ModuleLoadedEventArgs e)
        {
            var handler = ModuleLoaded;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ModuleUnloadedEventArgs> ModuleUnloaded;
        protected virtual void OnModuleUnloaded(ModuleUnloadedEventArgs e)
        {
            var handler = ModuleUnloaded;
            if (handler != null) handler(this, e);
        }

        // ----- Public methods
        public IEnumerable<Module> GetModules()
        {
            return _modules;
        }
        public Module GetModule(ModuleName moduleName)
        {
            return _modules.FirstOrDefault(x => Equals(x.Name, moduleName));
        }
        public ModuleName AddModule(string modulePath)
        {
            var module = new Module(modulePath);
            _modules.Add(module);
            return module.Name;
        }
        public void RemoveModule(ModuleName name)
        {
            var module = FindModule(name);
            if (module.IsLoaded) {
                UnloadModule(name);
            }
            _modules.Remove(module);
        }
        public void LoadModule(ModuleName name)
        {
            var module = FindModule(name);
            if (module.IsLoaded) {
                throw new ModuleException(string.Format("The module '{0}' is already loaded.", module.Name));
            }
            module.Load();
            OnModuleLoaded(new ModuleLoadedEventArgs(module));
        }
        public void UnloadModule(ModuleName name)
        {
            var module = FindModule(name);
            module.Unload();
            OnModuleUnloaded(new ModuleUnloadedEventArgs(module));
        }

        // ----- Utils
        private Module FindModule(ModuleName name)
        {
            var module = _modules.FirstOrDefault(x => Equals(x.Name, name));
            if (module == null) {
                throw new ModuleException(string.Format("The module '{0}' was not found.", name));
            }
            return module;
        }
    }
}