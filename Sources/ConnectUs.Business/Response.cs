namespace ConnectUs.Business
{
    public abstract class Response
    {
    }

    public class ErrorResponse : Response
    {
        public string Error { get; set; }
    }
}