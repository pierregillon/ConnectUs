namespace ConnectUs.Business
{
    public abstract class Response
    {
    }

    public class ErrorResponse : Response
    {
        public string Error { get; set; }
    }

    public class GetClientInformationResponse : Response
    {
        public string Ip { get; set; }
        public string MachineName { get; set; }
    }
}