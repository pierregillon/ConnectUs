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

        [When(@"The server requests to the client (.*) its information")]
        public void WhenTheServerRequestsToTheClientItsInformation(int index)
        {
            ClientInformation = Server
                .GetConnectedClients()
                .ElementAt(index - 1)
                .GetClientInformation();
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