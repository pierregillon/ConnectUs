using System;
using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class Client
    {
        private readonly IRequestProcessor _requestProcessor;

        public Client(IRequestProcessor requestProcessor)
        {
            _requestProcessor = requestProcessor;
        }

        public ClientInformation GetClientInformation()
        {
            throw new NotImplementedException();
            //var response = _requestProcessor.Process(new Request("GetClientInformation"));
            //if (response.Error != null) {
            //    throw new ClientException(response.Error);
            //}
            //return response.To<ClientInformation>();
        }
        public void CloseConnection()
        {
            _requestProcessor.Close();
        }
        public void Ping()
        {
            throw new NotImplementedException();
            try
            {
                //var response = _requestProcessor.Process(new Request("Ping"));
                //if (response.Error != null) {
                //    throw new ClientException(response.Error);
                //}
            }
            catch (ConnectionException ex) {
                throw new ClientException("Unable to execute the command 'Ping', the connection has been closed.", ex);
            }
        }
        public Response Execute(Request request)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var response = _requestProcessor.Process(request);
            //    if (response.Error != null) {
            //        throw new ClientException(response.Error);
            //    }
            //    return response;
            //}
            //catch (ConnectionException ex) {
            //    throw new ClientException(string.Format("Unable to execute the request '{0}', the connection has been closed.", request.Name), ex);
            //}
        }
    }
}