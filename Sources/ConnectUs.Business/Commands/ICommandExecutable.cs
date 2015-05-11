using System.Collections.Generic;

namespace ConnectUs.Business.Commands
{
    public interface ICommandExecutable
    {
        string Name { get; }
        string Execute(IEnumerable<RequestParameter> parameters);
        Request BuildRequest(IEnumerable<string> arguments);
        string DisplayResponse(Response response);
    }
}