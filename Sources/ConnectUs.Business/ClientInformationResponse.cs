using System.Net;

namespace ConnectUs.Business
{
    public class ClientInformationResponse : Message
    {
        public IPAddress Ip { get; set; }
    }
}