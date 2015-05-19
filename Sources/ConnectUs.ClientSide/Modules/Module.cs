using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConnectUs.ClientSide.Modules
{
    public class Module
    {
        private readonly string _modulepath;
        private AppDomain _moduleDomain;

        public string Path
        {
            get { return _modulepath; }
        }
        public string Name { get; private set; }
        public IEnumerable<object> Commands { get; private set; }

        // ----- Constructors
        public Module(string modulepath)
        {
            _modulepath = modulepath;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        // ----- Public methods
        public void Load()
        {
            _moduleDomain = AppDomain.CreateDomain("ModuleDomain");
            
            var assembly = _moduleDomain.Load(AssemblyName.GetAssemblyName(_modulepath));
            var moduleType = GetModuleFromAssembly(assembly);

            Name = assembly.GetName().Name;
            Commands = GetCommands(moduleType);
        }
        public void Unload()
        {
            AppDomain.Unload(_moduleDomain);
        }

        // ----- Event callbacks
        private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.LoadFile(_modulepath);
        }

        // ----- Internal logics
        private static Type GetModuleFromAssembly(Assembly assembly)
        {
            var moduleType = assembly.GetTypes().FirstOrDefault(x => x.Name == "Module");
            if (moduleType == null) {
                throw new Exception(string.Format("Unable to instanciate module '{0}'.", assembly.FullName));
            }
            return moduleType;
        }
        private static IEnumerable<object> GetCommands(Type moduleType)
        {
            var module = Activator.CreateInstance(moduleType);
            return (IEnumerable<object>) moduleType.GetMethod("GetCommands").Invoke(module, new object[0]);
        }
    }
}