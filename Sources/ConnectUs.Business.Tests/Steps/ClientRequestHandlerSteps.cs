using ConnectUs.Business.Connections;
using ConnectUs.Business.Tests.Mocks;
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

        public MockedClientRequestProcess MockedClientRequestProcess
        {
            get { return ScenarioContext.Current.Get<MockedClientRequestProcess>("MockedClientRequestProcess"); }
            set { ScenarioContext.Current.Add("MockedClientRequestProcess", value); }
        }

        [Given(@"A client request handler")]
        public void GivenAClientRequestHandler()
        {
            ClientRequestHandler = new ClientRequestHandler(MockedClientRequestProcess, new JsonRequestParser());
        }

        [When(@"I process the request from the client request handler")]
        public void WhenIProcessTheRequestFromTheClientRequestHandler()
        {
            ClientRequestHandler.ProcessNextRequestFrom(ClientConnection);
        }
    }
}