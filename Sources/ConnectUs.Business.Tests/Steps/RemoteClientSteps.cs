using ConnectUs.ClientSide.Commands.GetClientInformation;
using ConnectUs.ServerSide;
using Moq;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class RemoteClientSteps
    {
        public IRemoteClient RemoteClient
        {
            get { return ScenarioContext.Current.Get<IRemoteClient>("RemoteClient"); }
            set { ScenarioContext.Current.Add("RemoteClient", value); }
        }
        public IServerRequestCommunicator ServerRequestCommunicator
        {
            get { return ScenarioContext.Current.Get<IServerRequestCommunicator>("ServerRequestCommunicator"); }
            set { ScenarioContext.Current.Add("ServerRequestCommunicator", value); }
        }
        public GetClientInformationResponse GetClientInformationResponse
        {
            get { return ScenarioContext.Current.Get<GetClientInformationResponse>("GetClientInformationResponse"); }
            set { ScenarioContext.Current.Add("GetClientInformationResponse", value); }
        }
        public ClientException Exception
        {
            get { return ScenarioContext.Current.Get<ClientException>("Exception"); }
            set { ScenarioContext.Current.Add("Exception", value); }
        }
        public object Response
        {
            get { return ScenarioContext.Current.Get<object>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }

        [Given(@"A remote client on the request processor")]
        public void GivenAClientOnTheRequestProcessor()
        {
            RemoteClient = new RemoteClient(ServerRequestCommunicator);
        }

        [Given(@"A mocked remote client that returns the response")]
        public void GivenAMockedClientThatReturnsTheResponse()
        {
            var mock = new Mock<IRemoteClient>();
            mock.Setup(processor => processor.ExecuteCommand<GetClientInformationRequest, GetClientInformationResponse>(It.IsAny<GetClientInformationRequest>()))
                .Returns((GetClientInformationResponse)Response);
            RemoteClient = mock.Object;
        }

        [When(@"I ask the client information")]
        public void WhenIAskTheClientInformation()
        {
            try {
                GetClientInformationResponse = RemoteClient.ExecuteCommand<GetClientInformationRequest, GetClientInformationResponse>(new GetClientInformationRequest());
            }
            catch (ClientException ex) {
                Exception = ex;
            }
        }

        [Then(@"A client exception is thrown with the message ""(.*)""")]
        public void ThenAnRequestProcessorExceptionIsThrownWithTheMessage(string expectedMessage)
        {
            Check.That(Exception.Message).IsEqualTo(expectedMessage);
        }

        [Then(@"I get a client information")]
        public void ThenIGetAClientInformation()
        {
            Check.That(GetClientInformationResponse).IsNotNull();
        }

        [Then(@"the client information has the ip to ""(.*)""")]
        public void ThenTheClientInformationHasTheIpTo(string ip)
        {
            Check.That(GetClientInformationResponse.Ip).IsEqualTo(ip);
        }

        [Then(@"the client information has the machine name to ""(.*)""")]
        public void ThenTheClientInformationHasTheMachineNameTo(string machineName)
        {
            Check.That(GetClientInformationResponse.MachineName).IsEqualTo(machineName);
        }
    }
}