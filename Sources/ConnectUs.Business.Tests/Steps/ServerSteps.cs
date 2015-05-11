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
        public ClientInformation ClientInformation
        {
            get { return ScenarioContext.Current.Get<ClientInformation>("ClientInformation"); }
            set { ScenarioContext.Current.Add("ClientInformation", value); }
        }
        public Client Client
        {
            get { return ScenarioContext.Current.Get<Client>("Client"); }
            set { ScenarioContext.Current.Add("Client", value); }
        }
        public FakeClientListener ClientListener
        {
            get { return ScenarioContext.Current.Get<FakeClientListener>("ClientListener"); }
            set { ScenarioContext.Current.Add("ClientListener", value); }
        }

        [Given(@"A server")]
        public void GivenAServer()
        {
            ClientListener = new FakeClientListener();
            Server = new Server(ClientListener);
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
            ClientListener.AddClient(Client);
        }

        [When(@"The client disconnects from the server")]
        public void WhenTheClientDisconnectsFromTheServer()
        {
            ClientListener.RemoveClient(Client);
        }

        [Then(@"The server has (.*) client")]
        public void ThenTheServerHasClient(int clientCount)
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