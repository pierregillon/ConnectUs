using System.Threading;
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

        [Given(@"A server")]
        public void GivenAServer()
        {
            Connector = new FakeConnector();
            Server = new Server(Connector);
        }

        [When(@"The server start at the port (.*)")]
        public void WhenTheServerStartAtThePort(int port)
        {
            Server.Start(port);
        }

        [Then(@"The client list of the server has (.*) element")]
        public void ThenTheClientListOfTheServerHasElement(int clientCount)
        {
            Check.That(Server.Clients.Count()).IsEqualTo(clientCount);
        }

        [Then(@"The (.*) client has the ip ""(.*)""")]
        public void ThenTheClientHasTheIp(int index, string ip)
        {
            Check.That(Server.Clients.ElementAt(index-1).Ip.ToString()).IsEqualTo(ip);
        }

        [When(@"I wait (.*) seconds")]
        public void WhenIWaitSeconds(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

    }
}