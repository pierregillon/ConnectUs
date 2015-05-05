using ConnectUs.ServerSide;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ServerSteps
    {
        public Server Server { get; set; }

        [Given(@"A server")]
        public void GivenAServer()
        {
            Server = new Server();
        }

        [When(@"The server start at the port (.*)")]
        public void WhenTheServerStartAtThePort(int port)
        {
            Server.Start(port);
        }

        [Then(@"It appears in the client list of the server")]
        public void ThenItAppearsInTheClientListOfTheServer()
        {
            Check.That(Server.Clients.Count()).IsEqualTo(1);
        }
    }
}