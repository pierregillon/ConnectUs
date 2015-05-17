using System.Collections.Generic;
using System.Diagnostics;
using ConnectUs.Business.Commands.ClientInformation;

namespace ConnectUs.ServerSide.CommandLines
{
    public class PingCommandLine : ICommandLine
    {
        public string Name
        {
            get { return "ping"; }
        }

        public string ExecuteCommand(Client client, IEnumerable<string> parameters)
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