namespace ConnectUs.Business.Commands.ClientInformation
{
    public class PingCommand
    {
        public PingResponse Execute(PingRequest request)
        {
            return new PingResponse {Value = "OK"};
        }
    }
}