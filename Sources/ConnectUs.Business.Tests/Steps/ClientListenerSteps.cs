using ConnectUs.Business.Connections;
using ConnectUs.ServerSide;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ClientListenerSteps
    {
        public IConnectionListener ConnectionListener
        {
            get { return ScenarioContext.Current.Get<IConnectionListener>("ConnectionListener"); }
            set { ScenarioContext.Current.Add("ConnectionListener", value); }
        }
        public IClientListener ClientListener
        {
            get { return ScenarioContext.Current.Get<IClientListener>("ClientListener"); }
            set { ScenarioContext.Current.Add("ClientListener", value); }
        }
        public ServerConfiguration ServerConfiguration
        {
            get { return ScenarioContext.Current.Get<ServerConfiguration>("ServerConfiguration"); }
            set { ScenarioContext.Current.Add("ServerConfiguration", value); }
        }
        public ClientConnectedEventArgs ClientConnectedEventArgs
        {
            get { return ScenarioContext.Current.Get<ClientConnectedEventArgs>("ClientConnectedEventArgs"); }
            set { ScenarioContext.Current.Add("ClientConnectedEventArgs", value); }
        }
        public ClientDisconnectedEventArgs ClientDisconnectedEventArgs
        {
            get { return ScenarioContext.Current.Get<ClientDisconnectedEventArgs>("ClientDisconnectedEventArgs"); }
            set { ScenarioContext.Current.Add("ClientDisconnectedEventArgs", value); }
        }

        [Given(@"A client listener linked with the connection listener and the server parameters")]
        public void GivenAClientListenerLinkedWithTheConnectionListenerAndTheServerParameters()
        {
            ClientListener = new ClientListener(ConnectionListener);
            ClientListener.ClientConnected += (sender, args) => ClientConnectedEventArgs = args;
            ClientListener.ClientDisconnected += (sender, args) => ClientDisconnectedEventArgs = args;
        }

        [When(@"I start the client listener")]
        public void WhenIStartTheClientListener()
        {
            ClientListener.Start(ServerConfiguration.Port);
        }

        [Then(@"A new ClientConnected event is raised")]
        public void ThenANewClientConnectedEventIsRaised()
        {
            Check.That(ClientConnectedEventArgs).IsNotNull();
        }

        [Then(@"A client disconnected event is raised")]
        public void ThenAClientDisconnectedEventIsRaised()
        {
            Check.That(ClientDisconnectedEventArgs).IsNotNull();
        }

        [Then(@"the client of the connected event is the same of the disconnected event")]
        public void ThenTheClientOfTheConnectedEventIsTheSameOfTheDisconnectedEvent()
        {
            Check.That(ClientDisconnectedEventArgs.Client).IsEqualTo(ClientConnectedEventArgs.Client);
        }
    }
}