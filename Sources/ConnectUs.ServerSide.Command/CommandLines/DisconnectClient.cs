namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "disconnect", Description = "Disconnect from the current client.")]
    internal class DisconnectClient : CommandHandler, ICommandLineHandler
    {
        private readonly Context _context;

        public DisconnectClient(Context context)
        {
            _context = context;
        }

        public void Handle(CommandLine commandLine)
        {
            _context.CurrentClient = null;
        }
    }
}