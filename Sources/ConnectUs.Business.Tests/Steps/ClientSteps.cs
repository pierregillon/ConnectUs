using System.Net;
using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ClientSteps
    {
        public FakeConnector Connector
        {
            get { return ScenarioContext.Current.Get<FakeConnector>("FakeConnector"); }
            set { ScenarioContext.Current.Add("FakeConnector", value); }
        }
        public Client Client
        {
            get { return ScenarioContext.Current.Get<Client>("Client"); }
            set { ScenarioContext.Current.Add("Client", value); }
        }

        [Given(@"A client")]
        public void GivenAClient()
        {
            Client = new Client(Connector, new MessageProcessor(new FakeClientInformationService(IPAddress.None)));
        }

        [Given(@"A client with ip ""(.*)""")]
        public void GivenAClientWithIp(string ip)
        {
            Client = new Client(Connector, new MessageProcessor(new FakeClientInformationService(IPAddress.Parse(ip))));
        }

        [When(@"The client connects the server at the port (.*)")]
        public void WhenTheClientConnectsTheServerAtThePort(int p0)
        {
            Client.StartProcessRequest("localhost", 9000);
        }
    }
}