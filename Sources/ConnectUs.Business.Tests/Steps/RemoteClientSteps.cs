using ConnectUs.Modules.Integrated.GetClientInformation;
using ConnectUs.ServerSide.Clients;
using ConnectUs.ServerSide.Requests;
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
        public IRequestDispatcher RequestDispatcher
        {
            get { return ScenarioContext.Current.Get<IRequestDispatcher>("RequestDispatcher"); }
            set { ScenarioContext.Current.Add("RequestDispatcher", value); }
        }
        public GetClientInformationResponse GetClientInformationResponse
        {
            get { return ScenarioContext.Current.Get<GetClientInformationResponse>("GetClientInformationResponse"); }
            set { ScenarioContext.Current.Add("GetClientInformationResponse", value); }
        }
        public RemoteClientException Exception
        {
            get { return ScenarioContext.Current.Get<RemoteClientException>("Exception"); }
            set { ScenarioContext.Current.Add("Exception", value); }
        }
        public object Response
        {
            get { return ScenarioContext.Current.Get<object>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }

        [Given(@"A remote client on the server request dispatcher")]
        public void GivenAClientOnTheServerRequestDispatcher()
        {
            RemoteClient = new RemoteClient(RequestDispatcher);
        }

        [Given(@"A mocked remote client that returns the response")]
        public void GivenAMockedClientThatReturnsTheResponse()
        {
            var mock = new Mock<IRemoteClient>();
            mock.Setup(processor => processor.Send<GetClientInformationRequest, GetClientInformationResponse>(It.IsAny<GetClientInformationRequest>()))
                .Returns((GetClientInformationResponse)Response);
            RemoteClient = mock.Object;
        }

        [When(@"I ask the client information")]
        public void WhenIAskTheClientInformation()
        {
            try {
                GetClientInformationResponse = RemoteClient.Send<GetClientInformationRequest, GetClientInformationResponse>(new GetClientInformationRequest());
            }
            catch (RemoteClientException ex) {
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