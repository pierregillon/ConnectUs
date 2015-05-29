using ConnectUs.Business.Connections;
using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
using ConnectUs.ClientSide.Commands.GetClientInformation;
using ConnectUs.ServerSide;
using Moq;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ServerRequestProcessorSteps
    {
        public IConnection ServerConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ServerConnection"); }
            set { ScenarioContext.Current.Add("ServerConnection", value); }
        }
        public IServerRequestProcessor ServerRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IServerRequestProcessor>("ServerRequestProcessor"); }
            set { ScenarioContext.Current.Add("ServerRequestProcessor", value); }
        }
        public IServerRequestCommunicator ServerRequestCommunicator
        {
            get { return ScenarioContext.Current.Get<IServerRequestCommunicator>("ServerRequestCommunicator"); }
            set { ScenarioContext.Current.Add("ServerRequestCommunicator", value); }
        }
        public object Response
        {
            get { return ScenarioContext.Current.Get<object>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }

        // Given

        [Given(@"A server request processor is initialized")]
        public void GivenAServerRequestProcessorIsInitialized()
        {
            ServerRequestProcessor = new RemoteRequestProcessor(ServerRequestCommunicator, new ServerFileCommunicator(ServerConnection));
        }

        [Given(@"A mocked server request processor that returns the response")]
        public void GivenAMockedServerRequestProcessorThatReturnsTheResponse()
        {
            var mock = new Mock<IServerRequestProcessor>();
            mock.Setup(processor => processor.Process<GetClientInformationRequest, GetClientInformationResponse>(It.IsAny<GetClientInformationRequest>()))
                .Returns((GetClientInformationResponse) Response);
            ServerRequestProcessor = mock.Object;
        }
    }
}