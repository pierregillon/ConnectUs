namespace ConnectUs.ServerSide.Command
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
            return "Unknown command";
        }
    }
}