namespace ConnectUs.ServerSide.Command.CommandLines
{
    public abstract class CurrentClientCommand : ICommandLineHandler
    {
        private readonly Context _context;

        protected CurrentClientCommand(Context context)
        {
            _context = context;
        }

        public string Handle(CommandLine commandLine)
        {
            if (_context.CurrentClient == null) {
                return "You should define the current client first using 'connect %index'.";
            }

            return HandleInternal(commandLine, _context.CurrentClient);
        }

        protected abstract string HandleInternal(CommandLine commandLine, Client client);
    }
}