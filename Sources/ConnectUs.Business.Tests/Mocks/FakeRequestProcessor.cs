namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeRequestProcessor : IRequestProcessor
    {
        public Response Process(Request request)
        {
            return new ErrorResponse
            {
                Error = string.Format("The request '{0}' is unknown on the client.", request.Name)
            };
        }
        public void Close()
        {
            
        }
    }
}