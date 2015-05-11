using ConnectUs.Business.Commands;
using ConnectUs.Business.Commands.ClientInformation;
using ConnectUs.Business.Connections;
using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
using ConnectUs.ServerSide;
using Moq;
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
        public IServerRequestProcessor ServerRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IServerRequestProcessor>("ServerRequestProcessor"); }
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

        [Given(@"A client request processor is initialized to echo requests")]
        public void GivenAClientRequestProcessorIsInitializedToEchoRequests()
        {
            ContinuousRequestProcessor = new ContinuousRequestProcessor(new MockedEchoClientRequestProcessor());
        }

        [Given(@"A server request processor is initialized")]
        public void GivenAServerRequestProcessorIsInitialized()
        {
            ServerRequestProcessor = new RemoteRequestProcessor(new ServerRequestCommunicator(ServerConnection, new JsonRequestParser()));
        }

        [Given(@"A mocked server request processor that returns the response")]
        public void GivenAMockedServerRequestProcessorThatReturnsTheResponse()
        {
            var mock = new Mock<IServerRequestProcessor>();
            mock.Setup(processor => processor.Process<GetClientInformationRequest, GetClientInformationResponse>(It.IsAny<GetClientInformationRequest>()))
                .Returns((GetClientInformationResponse) Response);
            ServerRequestProcessor = mock.Object;
        }


        [When(@"I start the echo process on client")]
        public void WhenIStartTheEchoProcessOnClient()
        {
            ContinuousRequestProcessor.StartProcessingRequestFromConnection(ClientConnection);
        }

        [AfterScenario("ConcurrentRequestExecution")]
        public void AfterScenario()
        {
            ContinuousRequestProcessor.StopProcessingRequestFromConnection();
        }
    }
}