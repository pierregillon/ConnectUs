using System;
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
            if (requestName == "throwErrorRequest") {
                throw new Exception("An error occured!");
            }
            return null;
        }
    }
}