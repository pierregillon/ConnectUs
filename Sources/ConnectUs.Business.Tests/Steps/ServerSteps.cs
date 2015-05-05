using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ServerSide;
using NFluent;
using TechTalk.SpecFlow;
using System.Linq;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ServerSteps
    {
        public Server Server
        {
            get { return ScenarioContext.Current.Get<Server>("Server"); }
            set { ScenarioContext.Current.Add("Server", value); }
        }
        public FakeConnector Connector
        {
            get { return ScenarioContext.Current.Get<FakeConnector>("FakeConnector"); }
            set { ScenarioContext.Current.Add("FakeConnector", value); }
        }
        public ClientInformationResponse ClientInformation
        {
            get { return ScenarioContext.Current.Get<ClientInformationResponse>("ClientInformation"); }
            set { ScenarioContext.Current.Add("ClientInformation", value); }
        }
        public Client Client
        {
            get { return ScenarioContext.Current.Get<Client>("RequestProcessor"); }
            set { ScenarioContext.Current.Add("RequestProcessor", value); }
        }

        [Given(@"A server")]
        public void GivenAServer()
        {
            Connector = new FakeConnector();
            Server = new Server();
        }

        [When(@"The server requests to the client (.*) its information")]
        public void WhenTheServerRequestsToTheClientItsInformation(int index)
        {
            ClientInformation = Server
                .GetConnectedClients()
                .ElementAt(index - 1)
                .GetClientInformation();
        }

        [When(@"The client connects the server")]
        public void WhenTheClientConnectsTheServer()
        {
            Server.AddConnectedClient(Client);
        }

        [Then(@"The client list of the server has (.*) element")]
        public void ThenTheClientListOfTheServerHasElement(int clientCount)
        {
            Check.That(Server.GetConnectedClients().Count()).IsEqualTo(clientCount);
        }

        [Then(@"The received information contains an ip to ""(.*)""")]
        public void ThenTheReceivedInformationContainsAnIpTo(string ip)
        {
            Check.That(ClientInformation.Ip).IsEqualTo(ip);
        }

        [Then(@"The received information contains a machine name to ""(.*)""")]
        public void ThenTheReceivedInformationContainsAMachineNameTo(string machineName)
        {
            Check.That(ClientInformation.MachineName).IsEqualTo(machineName);
        }
    }
}