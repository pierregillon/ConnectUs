namespace ConnectUs.ClientSide.Commands.GetClientInformation
{
    public class GetClientInformationResponse
    {
        public string Ip { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public string OperatingSystem { get; set; }
    }
}