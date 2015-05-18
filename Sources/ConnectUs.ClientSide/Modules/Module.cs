using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectUs.ClientSide.Modules
{
    public class Module
    {
        private readonly string _modulepath;
        private AppDomain _moduleDomain;

        public string Path { get { return _modulepath; } }
        public string Name { get; private set; }
        public IEnumerable<object> Commands { get; private set; }

        public Module(string modulepath)
        {
            _modulepath = modulepath;
        }

        public void Load()
        {
            _moduleDomain = AppDomain.CreateDomain("ModuleDomain", AppDomain.CurrentDomain.Evidence, new AppDomainSetup {ApplicationBase = Environment.CurrentDirectory});
            var proxy = new Proxy();
            var assembly = proxy.LoadAssembly(_modulepath);
            var moduleType = assembly.GetTypes().FirstOrDefault(x => x.Name == "Module");
            if (moduleType == null) {
                throw new Exception(string.Format("Unable to instanciate module '{0}'.", assembly.FullName));
            }

            Name = assembly.GetName().Name;
            var module = Activator.CreateInstance(moduleType);
            Commands = (IEnumerable<object>) moduleType.GetMethod("GetCommands").Invoke(module, new object[0]);
        }
        public void Unload()
        {
            AppDomain.Unload(_moduleDomain);
        }
    }
}