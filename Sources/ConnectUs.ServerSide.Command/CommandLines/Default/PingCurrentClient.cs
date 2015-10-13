using System.Threading;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Default
{
    [CommandDescription(CommandName = "ping", Description = "Connect to a client.")]
    internal class PingCurrentClient : CurrentClientCommand
    {
        public PingCurrentClient(Context context) : base(context)
        {
        }

        protected override void HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var decorator = new ClientInformationDecorator(remoteClient);
            for (var i = 0; i < 5; i++) {
                var duration = decorator.Ping();
                WriteInfo(string.Format("Ping to client : {0} ms", duration));
                Thread.Sleep(500);
            }
        }
    }
}