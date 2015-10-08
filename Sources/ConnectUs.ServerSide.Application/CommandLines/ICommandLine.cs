using System.Collections.Generic;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public interface ICommandLine
    {
        string Name { get; }
        string ExecuteCommand(IRemoteClient remoteClient, IEnumerable<string> parameters);
    }
}