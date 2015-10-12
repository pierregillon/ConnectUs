using ConnectUs.ServerSide.Business;
using ConnectUs.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command.CommandLines.Default
{
    [CommandDescription(CommandName = "ping", Description = "Connect to a client.")]
    internal class PingCurrentClient : CurrentClientCommand
    {
        public PingCurrentClient(Context context) : base(context)
        {
        }

        protected override string HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var decorator = new ClientInformationDecorator(remoteClient);
            var duration = decorator.Ping();
            return string.Format("Ping to client : {0} ms", duration);
        }
    }
}