using System;

namespace ConnectUs.ServerSide.Command
{
    public class CommandLineProcessor
    {
        private readonly ICommandLineHandlerLocator _commandLineHandlerLocator;
        private readonly IConsole _console = new WindowConsole();

        public CommandLineProcessor(ICommandLineHandlerLocator commandLineHandlerLocator)
        {
            _commandLineHandlerLocator = commandLineHandlerLocator;
        }

        public void Execute(string command)
        {
            try {
                var commandLine = CommandLine.Parse(command);
                if (commandLine == null) {
                    return;
                }
                var commandLineHandler =_commandLineHandlerLocator.Get(commandLine.CommandName);
                if (commandLineHandler != null) {
                    commandLineHandler.Handle(commandLine);
                }
                else {
                    _console.WriteError("The command '{0}' is unknown. Did you forget to load some modules ? Type help for more information.", commandLine.CommandName);
                }
            }
            catch (Exception ex) {
                _console.WriteError(ex.Message);
            }
        }
    }
}