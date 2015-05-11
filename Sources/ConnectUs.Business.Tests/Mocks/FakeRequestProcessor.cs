namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeRequestProcessor : IRequestProcessor
    {
        public Response Process(Request request)
        {
            if (request.Name == "dir") {
                return new Response
                {
                    Result = "my result"
                };
            }
            return new Response
            {
                Error = string.Format("The request '{0}' is unknown on the client.", request.Name)
            };
        }
    }
}