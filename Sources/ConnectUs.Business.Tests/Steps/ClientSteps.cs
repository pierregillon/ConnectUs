using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ServerSide;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ClientSteps
    {
        public Client Client
        {
            get { return ScenarioContext.Current.Get<Client>("Client"); }
            set { ScenarioContext.Current.Add("Client", value); }
        }
        public IRequestProcessor ServerRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IRequestProcessor>("ServerRequestProcessor"); }
            set { ScenarioContext.Current.Add("ServerRequestProcessor", value); }
        }
        public ClientInformation ClientInformation
        {
            get { return ScenarioContext.Current.Get<ClientInformation>("ClientInformation"); }
            set { ScenarioContext.Current.Add("ClientInformation", value); }
        }
        public ClientException Exception
        {
            get { return ScenarioContext.Current.Get<ClientException>("Exception"); }
            set { ScenarioContext.Current.Add("Exception", value); }
        }

        [Given(@"A client")]
        public void GivenAClient()
        {
            Client = new Client(new FakeRequestProcessor());
        }

        [Given(@"A client on the request processor")]
        public void GivenAClientOnTheRequestProcessor()
        {
            Client = new Client(ServerRequestProcessor);
        }

        [When(@"I ask the client information")]
        public void WhenIAskTheClientInformation()
        {
            try {
                ClientInformation = Client.GetClientInformation();
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
            Check.That(ClientInformation).IsNotNull();
        }

        [Then(@"the client information has the ip to ""(.*)""")]
        public void ThenTheClientInformationHasTheIpTo(string ip)
        {
            Check.That(ClientInformation.Ip).IsEqualTo(ip);
        }

        [Then(@"the client information has the machine name to ""(.*)""")]
        public void ThenTheClientInformationHasTheMachineNameTo(string machineName)
        {
            Check.That(ClientInformation.MachineName).IsEqualTo(machineName);
        }
    }
}