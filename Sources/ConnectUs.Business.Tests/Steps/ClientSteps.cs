using ConnectUs.ClientSide;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ClientSteps
    {
        public Client Client { get; set; }

        [Given(@"A client")]
        public void GivenAClient()
        {
            Client = new Client();
        }

        [When(@"The client connects the server at the port (.*)")]
        public void WhenTheClientConnectsTheServerAtThePort(int p0)
        {
            Client.Connect("localhost", 9000);
        }
    }
}