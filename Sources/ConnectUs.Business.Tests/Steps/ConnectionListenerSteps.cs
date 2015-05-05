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

        [Given(@"A connection listener")]
        public void GivenAConnectionListener()
        {
            MockConnectionListener = new Mock<IConnectionListener>();
            MockConnectionListener.Setup(listener => listener.Start(It.IsAny<int>())).Callback<int>(port => PortUsed = port);
            ConnectionListener = MockConnectionListener.Object;
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

        [Then(@"The connection listener is started on port (.*)")]
        public void ThenTheConnectionListenerIsStartedOnPort(int port)
        {
            Check.That(PortUsed).IsEqualTo(port);
        }
    }
}