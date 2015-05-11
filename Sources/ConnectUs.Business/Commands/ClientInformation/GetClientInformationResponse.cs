namespace ConnectUs.Business.Commands.ClientInformation
{
    public class GetClientInformationResponse : Response
    {
        public string Ip { get; set; }
        public string MachineName { get; set; }
    }
}