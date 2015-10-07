using System.Collections.Generic;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public interface ICommandLine
    {
        string Name { get; }
        string ExecuteCommand(Client client, IEnumerable<string> parameters);
    }
}