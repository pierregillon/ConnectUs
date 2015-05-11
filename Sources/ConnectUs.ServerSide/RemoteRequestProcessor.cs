using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class RemoteRequestProcessor : IRequestProcessor
    {
        private readonly IConnection _connection;
        private readonly Queue<RequestParameter> _requests = new Queue<RequestParameter>();
        private readonly object _locker = new object();

        // ----- Constructors
        public RemoteRequestProcessor(IConnection connection)
        {
            _connection = connection;
        }

        // ----- Public methods
        public Response Process(Request request)
        {
            var parameter = new RequestParameter(request);
            EnqueueRequest(parameter);
            parameter.WaitResponse();
            if (parameter.Error != null) {
                throw parameter.Error;
            }
            return parameter.Response;
        }
        public void Close()
        {
            _connection.Dispose();
        }

        // ----- Internal logics
        private void EnqueueRequest(RequestParameter parameter)
        {
            lock (_locker) {
                _requests.Enqueue(parameter);
                if (_requests.Count == 1) {
                    StartProcessingRequest();
                }
            }
        }
        private RequestParameter DequeueRequest()
        {
            return _requests.Dequeue();
        }
        private void StartProcessingRequest()
        {
            var thread = new Thread(ExecuteRequests);
            thread.Start();
        }
        private void ExecuteRequests()
        {
            while (_requests.Any()) {
                var parameter = DequeueRequest();
                ExecuteRequest(parameter);
            }
        }
        private void ExecuteRequest(RequestParameter parameter)
        {
            try {
                _connection.Send(parameter.Request);
                parameter.Response = _connection.Read<Response>();
            }
            catch (Exception e) {
                parameter.Error = e;
            }
            finally {
                parameter.Notify();
            }
        }
    }
}