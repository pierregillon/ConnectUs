using ConnectUs.Business.Commands;

namespace ConnectUs.Business.Tests.Mocks
{
    public class EchoCommand : ICommand
    {
        public object Execute(string data)
        {
            return data;
        }
    }
}