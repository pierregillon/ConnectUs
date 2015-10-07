using System.Diagnostics;
using ConnectUs.ClientSide.Commands.Ping;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "ping", Description = "Connect to a client.")]
    internal class PingCurrentClient : CurrentClientCommand, ICommandLineHandler
    {
        public PingCurrentClient(Context context) : base(context)
        {
        }

        protected override string HandleInternal(CommandLine commandLine, Client client)
        {
            var watch = new Stopwatch();
            watch.Start();
            client.ExecuteCommand<PingRequest, PingResponse>(new PingRequest());
            watch.Stop();
            var duration = watch.ElapsedMilliseconds;
            return string.Format("Ping to client : {0} ms", duration);
        }
    }
}