using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConnectUs.ClientSide.ModuleManagement
{
    public class Module
    {
        private AppDomain _moduleDomain;
        private readonly AssemblyName _assemblyName;
        private ModuleName _name;
        private readonly string _modulePath;

        public string Path
        {
            get { return _modulePath; }
        }
        public ModuleName Name
        {
            get
            {
                if (_name == null) {
                    _name = new ModuleName(_assemblyName.Name);
                }
                return _name;
            }
        }
        public Version Version
        {
            get { return _assemblyName.Version; }
        }
        public IEnumerable<object> Commands { get; private set; }
        public bool IsLoaded { get; private set; }

        // ----- Constructors
        public Module(string modulepath)
        {
            try {
                _modulePath = modulepath;
                _assemblyName = AssemblyName.GetAssemblyName(modulepath);
            }
            catch (FileNotFoundException ex) {
                throw new ModuleException(string.Format("Unable to add the module '{0}' : file was not found.", modulepath), ex);
            }
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }
        ~Module()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomainOnAssemblyResolve;
        }

        // ----- Public methods
        public void Load()
        {
            _moduleDomain = AppDomain.CreateDomain("ModuleDomain");

            try {
                var assembly = _moduleDomain.Load(_assemblyName);
                var moduleType = GetModuleClassFromAssembly(assembly);
                Commands = GetCommands(moduleType);
            }
            catch (Exception) {
                AppDomain.Unload(_moduleDomain);
                throw;
            }
            IsLoaded = true;
        }
        public void Unload()
        {
            AppDomain.Unload(_moduleDomain);
            IsLoaded = false;
        }

        // ----- Event callbacks
        private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.LoadFile(Path);
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