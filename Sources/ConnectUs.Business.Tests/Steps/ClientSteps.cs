using ConnectUs.Business.Commands;
using ConnectUs.Business.Commands.ClientInformation;
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
        public IServerRequestProcessor ServerRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IServerRequestProcessor>("ServerRequestProcessor"); }
            set { ScenarioContext.Current.Add("ServerRequestProcessor", value); }
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

        [Given(@"A client on the request processor")]
        public void GivenAClientOnTheRequestProcessor()
        {
            Client = new Client(ServerRequestProcessor);
        }

        [When(@"I ask the client information")]
        public void WhenIAskTheClientInformation()
        {
            try {
                GetClientInformationResponse = Client.GetClientInformation();
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