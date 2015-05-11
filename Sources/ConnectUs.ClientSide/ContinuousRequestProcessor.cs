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

        public event EventHandler<ConnectionLostEventArgs> ConnectionLost;
        protected virtual void OnConnectionLost(ConnectionLostEventArgs e)
        {
            EventHandler<ConnectionLostEventArgs> handler = ConnectionLost;
            if (handler != null) handler(this, e);
        }

        public ContinuousRequestProcessor(IConnection connection, IRequestProcessor requestProcessor)
        {
            _connection = connection;
            _requestProcessor = requestProcessor;
        }

        public void Process()
        {
            try {
                while (_continueProcessing) {
                    try {
                        var request = _connection.Read<Request>();
                        var response = _requestProcessor.Process(request);
                        _connection.Send(response);
                    }
                    catch (NoDataToReadFromConnectionException) {
                        Thread.Sleep(1000);
                    }
                }
                _resetEvent.Set();
            }
            catch (ConnectionException) {
                _connection.Dispose();
                _resetEvent.Close();
                OnConnectionLost(new ConnectionLostEventArgs(_connection));
            }
        }

        public void StartProcessingRequestFromConnection()
        {
            _continueProcessing = true;
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