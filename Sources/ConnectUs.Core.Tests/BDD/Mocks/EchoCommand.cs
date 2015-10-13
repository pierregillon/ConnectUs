namespace ConnectUs.Core.Tests.BDD.Mocks
{
    public class EchoCommand
    {
        public EchoResponse Execute(EchoRequest request)
        {
            return new EchoResponse{Result = request.Value};
        }
    }
}