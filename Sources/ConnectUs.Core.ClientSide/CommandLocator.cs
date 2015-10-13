using System.Collections.Generic;
using System.Linq;
using ConnectUs.Core.ClientSide.Commands;
using ConnectUs.Core.ModuleManagement;
using ConnectUs.Modules.Integrated.FileTransfert;
using ConnectUs.Modules.Integrated.GetClientInformation;
using ConnectUs.Modules.Integrated.GetFullClientInformation;
using ConnectUs.Modules.Integrated.ModuleManagement;
using ConnectUs.Modules.Integrated.Ping;

namespace ConnectUs.Core.ClientSide
{
    public class CommandLocator : ICommandLocator
    {
        private readonly IModuleManager _moduleManager;
        
        private readonly Dictionary<ModuleName, Dictionary<string, object>> _moduleCommands = new Dictionary<ModuleName, Dictionary<string, object>>();
        private readonly Dictionary<string, object> _defaultCommands = new Dictionary<string, object>();

        // ----- Constructors
        public CommandLocator(IModuleManager moduleManager, IClientInformation clientInformation)
        {
            _moduleManager = moduleManager;
            _moduleManager.ModuleLoaded += ModuleManagerOnModuleLoaded;
            _moduleManager.ModuleUnloaded += ModuleManagerOnModuleUnloaded;

            foreach (var module in _moduleManager.GetModules()) {
                if (module.IsLoaded) {
                    LoadCommandsFromModule(module);
                }
            }

            _defaultCommands.Add(typeof(GetClientInformationRequest).Name, new GetInformationCommand());
            _defaultCommands.Add(typeof(GetFullClientInformationRequest).Name, new GetFullInformationCommand());
            _defaultCommands.Add(typeof(PingRequest).Name, new PingCommand());
            _defaultCommands.Add(typeof(AddModuleRequest).Name, new AddModuleCommand(_moduleManager));
            _defaultCommands.Add(typeof(ListModuleRequest).Name, new ListModuleCommand(_moduleManager));
            _defaultCommands.Add(typeof(LoadModuleRequest).Name, new LoadModuleCommand(_moduleManager));
            _defaultCommands.Add(typeof(UnloadModuleRequest).Name, new UnloadModuleCommand(_moduleManager));
            _defaultCommands.Add(typeof(UploadRequest).Name, new UploadCommand(clientInformation));
            _defaultCommands.Add(typeof(DownloadRequest).Name, new DownloadCommand(clientInformation));
        }
        ~CommandLocator()
        {
            _moduleManager.ModuleLoaded -= ModuleManagerOnModuleLoaded;
            _moduleManager.ModuleUnloaded -= ModuleManagerOnModuleUnloaded;
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
        private void ModuleManagerOnModuleLoaded(object sender, ModuleLoadedEventArgs args)
        {
            LoadCommandsFromModule(args.Module);
        }
        private void ModuleManagerOnModuleUnloaded(object sender, ModuleUnloadedEventArgs args)
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