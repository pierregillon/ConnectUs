using ConnectUs.Business.Connections;
using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
using ConnectUs.ServerSide;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class RequestProcessorSteps
    {
        public IConnection ServerConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ServerConnection"); }
            set { ScenarioContext.Current.Add("ServerConnection", value); }
        }
        public IConnection ClientConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ClientConnection"); }
            set { ScenarioContext.Current.Add("ClientConnection", value); }
        }
        public IRequestProcessor ClientRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IRequestProcessor>("ClientRequestProcessor"); }
            set { ScenarioContext.Current.Add("ClientRequestProcessor", value); }
        }
        public IRequestProcessor ServerRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IRequestProcessor>("ServerRequestProcessor"); }
            set { ScenarioContext.Current.Add("ServerRequestProcessor", value); }
        }
        public ContinuousRequestProcessor ContinuousRequestProcessor
        {
            get { return ScenarioContext.Current.Get<ContinuousRequestProcessor>("ContinuousRequestProcessor"); }
            set { ScenarioContext.Current.Add("ContinuousRequestProcessor", value); }
        }
        public Request Request
        {
            get { return ScenarioContext.Current.Get<Request>("Request"); }
            set { ScenarioContext.Current.Add("Request", value); }
        }
        public Response Response
        {
            get { return ScenarioContext.Current.Get<Response>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }

        [Given(@"A client request processor is initialized")]
        public void GivenAClientRequestProcessorIsInitialized()
        {
            ClientRequestProcessor = new FakeRequestProcessor();
        }

        [Given(@"A server request processor is initialized")]
        public void GivenAServerRequestProcessorIsInitialized()
        {
            ServerRequestProcessor = new RemoteRequestProcessor(ServerConnection);
        }

        [When(@"I start the client continuous request processor")]
        public void WhenIStartTheClientContinuousRequestProcessor()
        {
            ContinuousRequestProcessor = new ContinuousRequestProcessor(ClientConnection, ClientRequestProcessor);
            ContinuousRequestProcessor.StartProcessingRequestFromConnection();
        }

        [When(@"I process the request in the server request processor")]
        public void WhenIProcessTheRequestInTheServerRequestProcessor()
        {
            Response = ServerRequestProcessor.Process(Request);
        }

        [AfterScenario("RequestProcessor")]
        public void AfterScenario()
        {
            ContinuousRequestProcessor.StopProcessingRequestFromConnection();
        }
    }
}