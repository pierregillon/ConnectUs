using ConnectUs.Business.Connections;
using ConnectUs.ServerSide;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class RemoteClientListenerSteps
    {
        public IConnectionListener ConnectionListener
        {
            get { return ScenarioContext.Current.Get<IConnectionListener>("ConnectionListener"); }
            set { ScenarioContext.Current.Add("ConnectionListener", value); }
        }
        public IRemoteClientListener RemoteClientListener
        {
            get { return ScenarioContext.Current.Get<IRemoteClientListener>("RemoteClientListener"); }
            set { ScenarioContext.Current.Add("RemoteClientListener", value); }
        }
        public int Port
        {
            get { return ScenarioContext.Current.Get<int>("Port"); }
            set { ScenarioContext.Current.Add("Port", value); }
        }
        public RemoteClientConnectedEventArgs RemoteClientConnectedEventArgs
        {
            get { return ScenarioContext.Current.Get<RemoteClientConnectedEventArgs>("RemoteClientConnectedEventArgs"); }
            set { ScenarioContext.Current.Add("RemoteClientConnectedEventArgs", value); }
        }
        public RemoteClientDisconnectedEventArgs RemoteClientDisconnectedEventArgs
        {
            get { return ScenarioContext.Current.Get<RemoteClientDisconnectedEventArgs>("RemoteClientDisconnectedEventArgs"); }
            set { ScenarioContext.Current.Add("RemoteClientDisconnectedEventArgs", value); }
        }

        [Given(@"A client listener linked with the connection listener and the server parameters")]
        public void GivenAClientListenerLinkedWithTheConnectionListenerAndTheServerParameters()
        {
            RemoteClientListener = new RemoteClientListener(ConnectionListener);
            RemoteClientListener.ClientConnected += (sender, args) => RemoteClientConnectedEventArgs = args;
            RemoteClientListener.ClientDisconnected += (sender, args) => RemoteClientDisconnectedEventArgs = args;
        }

        [When(@"I start the client listener")]
        public void WhenIStartTheClientListener()
        {
            RemoteClientListener.Start(Port);
        }

        [Then(@"A new ClientConnected event is raised")]
        public void ThenANewClientConnectedEventIsRaised()
        {
            Check.That(RemoteClientConnectedEventArgs).IsNotNull();
        }

        [Then(@"A client disconnected event is raised")]
        public void ThenAClientDisconnectedEventIsRaised()
        {
            Check.That(RemoteClientDisconnectedEventArgs).IsNotNull();
        }

        [Then(@"the client of the connected event is the same of the disconnected event")]
        public void ThenTheClientOfTheConnectedEventIsTheSameOfTheDisconnectedEvent()
        {
            Check.That(RemoteClientDisconnectedEventArgs.RemoteClient).IsEqualTo(RemoteClientConnectedEventArgs.RemoteClient);
        }
    }
}