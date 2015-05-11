using ConnectUs.Business.Connections;
using ConnectUs.Business.Tests.Mocks;
using Moq;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ConnectionListenerSteps
    {
        public IConnectionListener ConnectionListener
        {
            get { return ScenarioContext.Current.Get<IConnectionListener>("ConnectionListener"); }
            set { ScenarioContext.Current.Add("ConnectionListener", value); }
        }
        public Mock<IConnectionListener> MockConnectionListener
        {
            get { return ScenarioContext.Current.Get<Mock<IConnectionListener>>("MockConnectionListener"); }
            set { ScenarioContext.Current.Add("MockConnectionListener", value); }
        }
        public int PortUsed
        {
            get { return ScenarioContext.Current.Get<int>("PortUsed"); }
            set { ScenarioContext.Current.Add("PortUsed", value); }
        }
        public IConnection Connection
        {
            get { return ScenarioContext.Current.Get<IConnection>("Connection"); }
            set { ScenarioContext.Current.Add("Connection", value); }
        }
        public ConnectionEstablishedEventArgs ConnectionEstablishedEventArgs
        {
            get { return ScenarioContext.Current.Get<ConnectionEstablishedEventArgs>("ConnectionEstablishedEventArgs"); }
            set { ScenarioContext.Current.Add("ConnectionEstablishedEventArgs", value); }
        }


        [Given(@"A connection listener")]
        public void GivenAConnectionListener()
        {
            MockConnectionListener = new Mock<IConnectionListener>();
            MockConnectionListener.Setup(listener => listener.Start(It.IsAny<int>())).Callback<int>(port => PortUsed = port);
            ConnectionListener = MockConnectionListener.Object;
        }

        [Given(@"A socket connection listener")]
        public void GivenASocketConnectionListener()
        {
            ConnectionListener = new TcpClientConnectionListener();
            ConnectionListener.ConnectionEstablished += (sender, args) => ConnectionEstablishedEventArgs = args;
        }

        [When(@"a connection is established")]
        public void WhenAConnectionIsEstablished()
        {
            Connection = new FakeConnection();
            MockConnectionListener.Raise(listener => listener.ConnectionEstablished += null, new ConnectionEstablishedEventArgs(Connection));
        }

        [When(@"the connection is lost")]
        public void WhenTheConnectionIsLost()
        {
            MockConnectionListener.Raise(listener => listener.ConnectionLost += null, new ConnectionLostEventArgs(Connection));
        }

        [When(@"The connection listener starts listening on the port (.*)")]
        public void WhenTheConnectionListenerStartsListeningOnThePort(int port)
        {
            ConnectionListener.Start(port);
        }

        [When(@"I send the '(.*)' through the connection of the connection established event")]
        public void WhenISendTheThroughTheConnectionOfTheConnectionEstablishedEvent(string data)
        {
            ConnectionEstablishedEventArgs.Connection.Send(data);
        }

        [Then(@"The connection listener is started on port (.*)")]
        public void ThenTheConnectionListenerIsStartedOnPort(int port)
        {
            Check.That(PortUsed).IsEqualTo(port);
        }

        [Then(@"The connection listener raises a connection established event")]
        public void ThenTheConnectionListenerRaisesAConnectionEstablishedEvent()
        {
            Check.That(ConnectionEstablishedEventArgs).IsNotNull();
        }

        [Then(@"The connection established raised contains a connection")]
        public void ThenTheConnectionEstablishedRaisedContainsAConnection()
        {
            Check.That(ConnectionEstablishedEventArgs.Connection).IsNotNull();
        }

        [AfterScenario("ConnectionListener")]
        public void Clean()
        {
            if (ConnectionListener != null) {
                ConnectionListener.Stop();
            }
        }
    }
}