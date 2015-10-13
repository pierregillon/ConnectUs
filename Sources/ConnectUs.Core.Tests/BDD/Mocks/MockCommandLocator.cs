using ConnectUs.Core.ClientSide;

namespace ConnectUs.Core.Tests.BDD.Mocks
{
    public class MockCommandLocator : ICommandLocator
    {
        public object GetCommand(string requestName)
        {
            if (requestName == typeof(EchoRequest).Name) {
                return new EchoCommand();
            }
            return null;
        }
    }
}