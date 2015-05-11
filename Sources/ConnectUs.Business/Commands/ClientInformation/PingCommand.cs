namespace ConnectUs.Business.Commands.ClientInformation
{
    public class PingCommand : CommandBase<PingRequest, PingResponse>
    {
        protected override PingResponse ExecuteRequest(PingRequest request)
        {
            return new PingResponse {Value = "OK"};
        }
    }
}