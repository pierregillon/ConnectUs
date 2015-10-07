﻿namespace ConnectUs.ServerSide.Command
{
    public class CommandLineProcessor
    {
        private readonly ICommandLineHandlerLocator _commandLineHandlerLocator;

        public CommandLineProcessor(ICommandLineHandlerLocator commandLineHandlerLocator)
        {
            _commandLineHandlerLocator = commandLineHandlerLocator;
        }

        public string Execute(string command)
        {
            var commandLine = CommandLine.Parse(command);
            var commandLineHandler =_commandLineHandlerLocator.Get(commandLine.CommandName);
            if (commandLineHandler != null) {
                return commandLineHandler.Handle(commandLine);
            }
            return string.Format("The command '{0}' is unknown. Did you forget to load some modules ? Type help for more information.", commandLine.CommandName);
        }
    }
}