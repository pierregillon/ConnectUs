namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "disconnect", Description = "Disconnect from the current client.")]
    internal class DisconnectClient : ICommandLineHandler
    {
        private readonly Context _context;

        public DisconnectClient(Context context)
        {
            _context = context;
        }

        public string Handle(CommandLine commandLine)
        {
            _context.CurrentClient = null;
            return string.Empty;
        }
    }
}