﻿using System.Linq;
using ConnectUs.Core.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command.CommandLines.Server
{
    [CommandDescription(CommandName = "start", Description = "Start the listen of new clients.", Category = "Server")]
    internal class StartListeningClient : CommandHandler, ICommandLineHandler
    {
        private readonly IRemoteClientListener _remoteClientListener;
        private const int DefaultPort = 9000;

        public StartListeningClient(IRemoteClientListener remoteClientListener)
        {
            _remoteClientListener = remoteClientListener;
        }

        public void Handle(CommandLine commandLine)
        {
            var port = DefaultPort;
            var portArgument = commandLine.Arguments.FirstOrDefault(x => x.Name == "port");
            if (portArgument != null) {
                port = int.Parse(portArgument.Value);
            }
            _remoteClientListener.Start(port);
            WriteInfo(string.Format("Server started on port {0}.", port));
        }
    }
}