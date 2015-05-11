using System;
using ConnectUs.Business;
using ConnectUs.Business.Commands;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class Client
    {
        private readonly IServerRequestProcessor _serverRequestProcessor;

        public Client(IServerRequestProcessor serverRequestProcessor)
        {
            _serverRequestProcessor = serverRequestProcessor;
        }

        public GetClientInformationResponse GetClientInformation()
        {
            return _serverRequestProcessor.Process<GetClientInformationRequest, GetClientInformationResponse>(new GetClientInformationRequest());
        }
        public void CloseConnection()
        {
            _serverRequestProcessor.Close();
        }
        public void Ping()
        {
            throw new NotImplementedException();
            try
            {
                //var response = _serverRequestProcessor.Process(new Request("Ping"));
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
            //    var response = _serverRequestProcessor.Process(request);
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