using System;
using System.Threading;
using ConnectUs.Business;

namespace ConnectUs.ServerSide
{
    public class RequestParameter
    {
        private readonly ManualResetEvent _manualResetEvent;
        public Request Request { get; private set; }
        public Response Response { get; set; }
        public Exception Error { get; set; }

        public RequestParameter(Request request)
        {
            _manualResetEvent = new ManualResetEvent(false);
            Request = request;
        }

        public void WaitResponse()
        {
            _manualResetEvent.WaitOne();
        }
        public void Notify()
        {
            _manualResetEvent.Set();
        }
    }
}