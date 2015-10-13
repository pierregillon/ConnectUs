using ConnectUs.Core.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    public abstract class CurrentClientCommand : CommandHandler, ICommandLineHandler
    {
        private readonly Context _context;

        protected CurrentClientCommand(Context context)
        {
            _context = context;
        }

        public void Handle(CommandLine commandLine)
        {
            if (_context.CurrentClient == null) {
                WriteError("You should define the current client first using 'connect %index'.");
            }
            HandleInternal(commandLine, _context.CurrentClient.RemoteClient);
        }

        protected abstract void HandleInternal(CommandLine commandLine, IRemoteClient remoteClient);
    }
}