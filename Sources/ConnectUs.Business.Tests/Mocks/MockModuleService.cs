using ConnectUs.Business.Commands;
using ConnectUs.ClientSide;

namespace ConnectUs.Business.Tests.Mocks
{
    public class MockModuleService : IModuleService
    {
        public ICommand GetCommand(string requestName)
        {
            if (requestName == "echoRequest") {
                return new EchoCommand();
            }
            return null;
        }
    }
}