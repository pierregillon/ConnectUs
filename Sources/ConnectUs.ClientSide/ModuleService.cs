using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ConnectUs.Business.Commands.ClientInformation;

namespace ConnectUs.ClientSide
{
    public class ModuleService : IModuleService
    {
        private readonly List<string> _modulePaths = new List<string>
        {
            @"C:\Dev\Perso\ConnectUs\Sources\ConnectUs.FileExplorer\bin\Debug\ConnectUs.FileExplorer.dll"
        };
        private readonly Dictionary<string, object> _defaultCommands = new Dictionary<string, object>
        {
            {typeof (GetClientInformationRequest).Name, new GetInformationCommand()},
            {typeof (PingRequest).Name, new PingCommand()},
        };
        private readonly Dictionary<string, Dictionary<string, object>> _moduleCommands = new Dictionary<string, Dictionary<string, object>>();
        private AppDomain _moduleDomain;

        public void LoadModules()
        {
            _moduleDomain = AppDomain.CreateDomain("ModuleDomain", AppDomain.CurrentDomain.Evidence, new AppDomainSetup {ApplicationBase = Environment.CurrentDirectory});
            var proxy = new Proxy();
            foreach (var modulePath in _modulePaths) {
                var assembly = proxy.LoadAssembly(modulePath);
                LoadModule(assembly);
            }
        }
        public void UnloadModules()
        {
            AppDomain.Unload(_moduleDomain);
        }
        public object GetCommand(string requestName)
        {
            object command;
            if (_defaultCommands.TryGetValue(requestName, out command) == false) {
                foreach (var commandsByModule in _moduleCommands.Values) {
                    if (commandsByModule.TryGetValue(requestName, out command)) {
                        break;
                    }
                }
            }
            return command;
        }

        private void LoadModule(Assembly assembly)
        {
            var moduleType = assembly.GetTypes().FirstOrDefault(x => x.Name == "Module");
            if (moduleType == null) {
                throw new Exception(string.Format("Unable to instanciate module '{0}'.", assembly.FullName));
            }

            var moduleName = assembly.GetName().Name;
            _moduleCommands.Add(moduleName, new Dictionary<string, object>());

            var module = Activator.CreateInstance(moduleType);
            var commands = (IEnumerable<object>) moduleType.GetMethod("GetCommands").Invoke(module, new object[0]);
            foreach (var command in commands) {
                var parameterType = command.GetType().GetMethod("Execute").GetParameters().First().ParameterType;
                _moduleCommands[moduleName].Add(parameterType.Name, command);
            }
        }
    }

    public class Proxy : MarshalByRefObject
    {
        public Assembly LoadAssembly(string assemblyPath)
        {
            return Assembly.LoadFile(assemblyPath);
        }
    }
}