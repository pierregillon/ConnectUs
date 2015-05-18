namespace ConnectUs.Business.Tests.Mocks
{
    public class EchoCommand
    {
        public EchoResponse Execute(EchoRequest request)
        {
            return new EchoResponse{Result = request.Value};
        }
    }
}