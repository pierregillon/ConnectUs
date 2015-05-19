using System.Collections.Generic;
using System.Linq;
using ConnectUs.ClientSide.Modules;
using ConnectUs.Common.GetClientInformation;
using ConnectUs.Common.LoadModule;
using ConnectUs.Common.Ping;
using IModuleManager = ConnectUs.ClientSide.Modules.IModuleManager;

namespace ConnectUs.ClientSide
{
    public class CommandLocator : ICommandLocator
    {
        private readonly IModuleManager _moduleManager;
        private readonly Dictionary<string, Dictionary<string, object>> _moduleCommands = new Dictionary<string, Dictionary<string, object>>();
        private readonly Dictionary<string, object> _defaultCommands = new Dictionary<string, object>
        {
            {typeof (GetClientInformationRequest).Name, new GetInformationCommand()},
            {typeof (PingRequest).Name, new PingCommand()},
        };

        // ----- Constructors
        public CommandLocator(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
            foreach (var module in _moduleManager.GetModules()) {
                LoadCommandsFromModule(module);
            }
            _moduleManager.ModuleAdded += ModuleManagerOnModuleAdded;
            _moduleManager.ModuleRemoved += ModuleManagerOnModuleRemoved;

            _defaultCommands.Add(typeof(LoadModuleRequest).Name, new LoadModuleCommand(_moduleManager));
        }

        // ----- Public methods
        public object GetCommand(string requestName)
        {
            var command = GetDefaultCommand(requestName);
            if (command == null) {
                command = GetModuleCommand(requestName);
            }
            return command;
        }

        // ----- Event callbacks
        private void ModuleManagerOnModuleAdded(object sender, ModuleAddedEventArgs args)
        {
            LoadCommandsFromModule(args.Module);
        }
        private void ModuleManagerOnModuleRemoved(object sender, ModuleRemovedEventArgs args)
        {
            _moduleCommands.Remove(args.Module.Name);
        }

        // ----- Internal logics
        private object GetDefaultCommand(string requestName)
        {
            object command;
            _defaultCommands.TryGetValue(requestName, out command);
            return command;
        }
        private object GetModuleCommand(string requestName)
        {
            object command = null;
            foreach (var commandsByModule in _moduleCommands.Values) {
                if (commandsByModule.TryGetValue(requestName, out command)) {
                    break;
                }
            }
            return command;
        }
        private void LoadCommandsFromModule(Module module)
        {
            _moduleCommands.Add(module.Name, new Dictionary<string, object>());
            foreach (var command in module.Commands) {
                var parameterType = command.GetType().GetMethod("Execute").GetParameters().First().ParameterType;
                _moduleCommands[module.Name].Add(parameterType.Name, command);
            }
        }
    }
}