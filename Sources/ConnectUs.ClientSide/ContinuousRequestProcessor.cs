using System;
using System.Threading;
using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide
{
    public class ContinuousRequestProcessor
    {
        private readonly IConnection _connection;
        private readonly IRequestProcessor _requestProcessor;
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private bool _continueProcessing = true;

        public ContinuousRequestProcessor(IConnection connection, IRequestProcessor requestProcessor)
        {
            _connection = connection;
            _requestProcessor = requestProcessor;
        }

        public void Process()
        {
            while (_continueProcessing) {
                try {
                    var request = _connection.Read<Request>();
                    var response = _requestProcessor.Process(request);
                    _connection.Send(response);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
            }
            _resetEvent.Set();
        }

        public void StartProcessingRequestFromConnection()
        {
            var thread = new Thread(Process);
            thread.Start();
        }
        public void StopProcessingRequestFromConnection()
        {
            _continueProcessing = false;
            _resetEvent.WaitOne();
        }
    }
}