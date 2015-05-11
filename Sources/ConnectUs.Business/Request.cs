
namespace ConnectUs.Business
{
    public class Request
    {
        public string Name { get; set; }

        public Request(string name)
        {
            Name = name;
        }
    }

    public class GetClientInformationRequest : Request
    {
        public GetClientInformationRequest()
            : base("GetClientInformation")
        {
        }
    }
}