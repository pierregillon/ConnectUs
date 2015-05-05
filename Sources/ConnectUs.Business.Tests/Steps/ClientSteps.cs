using System.Net;
using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ServerSide;
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
            Client = new Client(new ClientSide.Client(new FakeClientInformationService(IPAddress.None, string.Empty)));
        }

        [Given(@"A client with an ip to '(.*)' and a machine name to '(.*)'")]
        public void GivenAClientWithAnIpToAndAMachineNameTo(string ip, string machineName)
        {
            Client = new Client(new ClientSide.Client(new FakeClientInformationService(IPAddress.Parse(ip), machineName)));
        }
    }
}