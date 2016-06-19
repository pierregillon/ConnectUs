using System;
using System.Collections.Generic;
using ConnectUs.Core.ServerSide.Clients;

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
            catch (RemoteClientException ex) {
                var messages = GetMessages(ex);
                _console.WriteError(messages);
            }
        }

        private string GetMessages(Exception exception)
        {
            var messages = new List<string>();
            while (exception != null) {
                messages.Add(exception.Message);
                exception = exception.InnerException;
            }
            return string.Join(Environment.NewLine, messages);
        }
    }
}