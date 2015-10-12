namespace ConnectUs.Modules.Integrated.Ping
{
    public class PingCommand
    {
        public PingResponse Execute(PingRequest request)
        {
            return new PingResponse {Value = "OK"};
        }
    }
}