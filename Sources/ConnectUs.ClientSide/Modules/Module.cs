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
        public Version Version { get; private set; }

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

            try {
                var assembly = _moduleDomain.Load(AssemblyName.GetAssemblyName(_modulepath));
                var moduleType = GetModuleClassFromAssembly(assembly);

                Name = assembly.GetName().Name;
                Version = assembly.GetName().Version;
                Commands = GetCommands(moduleType);
            }
            catch (Exception) {
                AppDomain.Unload(_moduleDomain);
                throw;
            }
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
        private static Type GetModuleClassFromAssembly(Assembly assembly)
        {
            var moduleType = assembly.GetTypes().FirstOrDefault(x => x.Name == "Module");
            if (moduleType == null) {
                throw new ModuleException(string.Format("The assembly '{0}' is not a valid module. No 'Module' class has been found.", assembly.GetName().Name));
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