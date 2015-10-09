using System.Collections.Generic;
using ConnectUs.ServerSide.Clients;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public interface ICommandLine
    {
        string Name { get; }
        string ExecuteCommand(IRemoteClient remoteClient, IEnumerable<string> parameters);
    }
}