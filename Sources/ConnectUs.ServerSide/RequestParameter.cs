using System.Threading;
using ConnectUs.Business;

namespace ConnectUs.ServerSide
{
    public class RequestParameter
    {
        private readonly ManualResetEvent _manualResetEvent;
        public Request Request { get; private set; }
        public Response Response { get; set; }

        public RequestParameter(Request request)
        {
            _manualResetEvent = new ManualResetEvent(false);
            Request = request;
        }

        public Response WaitResponse()
        {
            _manualResetEvent.WaitOne();
            return Response;
        }
        public void Notify()
        {
            _manualResetEvent.Set();
        }
    }
}