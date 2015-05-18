using ConnectUs.ClientSide;

namespace ConnectUs.Business.Tests.Mocks
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