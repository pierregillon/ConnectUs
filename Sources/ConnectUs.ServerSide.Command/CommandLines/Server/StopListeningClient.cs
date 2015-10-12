using ConnectUs.Core.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command.CommandLines.Server
{
    [CommandDescription(CommandName = "stop", Description = "Stop the listen of new clients.", Category = "Server")]
    internal class StopListeningClient : ICommandLineHandler
    {
        private readonly IRemoteClientListener _remoteClientListener;

        public StopListeningClient(IRemoteClientListener remoteClientListener)
        {
            _remoteClientListener = remoteClientListener;
        }

        public string Handle(CommandLine commandLine)
        {
            _remoteClientListener.Stop();
            return "Server stopped.";
        }
    }
}