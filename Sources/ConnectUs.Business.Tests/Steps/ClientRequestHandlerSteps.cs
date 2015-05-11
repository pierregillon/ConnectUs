using ConnectUs.Business.Connections;
using ConnectUs.ClientSide;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ClientRequestHandlerSteps
    {
        public IClientRequestHandler ClientRequestHandler
        {
            get { return ScenarioContext.Current.Get<IClientRequestHandler>("ClientRequestHandler"); }
            set { ScenarioContext.Current.Add("ClientRequestHandler", value); }
        }
        public IConnection ClientConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ClientConnection"); }
            set { ScenarioContext.Current.Add("ClientConnection", value); }
        }

        public IClientRequestProcessor ClientRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IClientRequestProcessor>("ClientRequestProcessor"); }
            set { ScenarioContext.Current.Add("ClientRequestProcessor", value); }
        }

        [Given(@"A client request handler")]
        public void GivenAClientRequestHandler()
        {
            ClientRequestHandler = new ClientRequestHandler(ClientRequestProcessor, new JsonRequestParser());
        }

        [When(@"I process the request from the client request handler")]
        public void WhenIProcessTheRequestFromTheClientRequestHandler()
        {
            ClientRequestHandler.ProcessNextRequestFrom(ClientConnection);
        }
    }
}