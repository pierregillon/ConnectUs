using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectUs.ClientSide.Modules
{
    public class ModuleManager : IModuleManager
    {
        private readonly List<Module> _modules = new List<Module>();

        public event EventHandler<ModuleAddedEventArgs> ModuleAdded;
        protected virtual void OnModuleAdded(ModuleAddedEventArgs e)
        {
            var handler = ModuleAdded;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ModuleRemovedEventArgs> ModuleRemoved;
        protected virtual void OnModuleRemoved(ModuleRemovedEventArgs e)
        {
            var handler = ModuleRemoved;
            if (handler != null) handler(this, e);
        }

        // ----- Public methods
        public void AddModule(string modulePath)
        {
            var module = new Module(modulePath);
            module.Load();
            _modules.Add(module);
            OnModuleAdded(new ModuleAddedEventArgs(module));
        }
        public void RemoveModule(string modulePath)
        {
            var module = _modules.FirstOrDefault(x => x.Path == modulePath);
            if (module == null) {
                throw new Exception("Module not found");
            }
            module.Unload();
            _modules.Remove(module);
            OnModuleRemoved(new ModuleRemovedEventArgs(module));
        }
        public IEnumerable<Module> GetModules()
        {
            return _modules;
        }
    }
}