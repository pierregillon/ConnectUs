using System.Diagnostics;
using ConnectUs.ClientSide.Commands.Ping;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "ping", Description = "Connect to a client.")]
    internal class PingCurrentClient : ICommandLineHandler
    {
        private readonly Context _context;

        public PingCurrentClient(Context context)
        {
            _context = context;
        }

        public string Handle(CommandLine commandLine)
        {
            if (_context.CurrentClient == null) {
                return "You should define the current client first using 'connect %index'.";
            }

            var watch = new Stopwatch();
            watch.Start();
            _context.CurrentClient.ExecuteCommand<PingRequest, PingResponse>(new PingRequest());
            watch.Stop();
            var duration = watch.ElapsedMilliseconds;
            return string.Format("Ping to client : {0} ms", duration);
        }
    }
}