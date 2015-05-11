namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeRequestProcessor : IRequestProcessor
    {
        public Response Process(Request request)
        {
            if (request.Name == "GetClientInformation") {
                return new Response
                {
                    Result = "{\"Ip\" : \"192.168.1.1\", \"MachineName\" : \"mycomputer\"}"
                };
            }

            if (request.Name.StartsWith("Request")) {
                var id = request.Name.Substring("Request".Length);
                return new Response
                {
                    Result = "Response" + id
                };
            }

            return new Response
            {
                Error = string.Format("The request '{0}' is unknown on the client.", request.Name)
            };
        }
        public void Close()
        {
            
        }
    }
}