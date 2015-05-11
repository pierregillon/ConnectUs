using ConnectUs.Business.Commands;
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
        public GetClientInformationResponse GetClientInformationResponse
        {
            get { return ScenarioContext.Current.Get<GetClientInformationResponse>("GetClientInformationResponse"); }
            set { ScenarioContext.Current.Add("GetClientInformationResponse", value); }
        }

        [When(@"The server requests to the client (.*) its information")]
        public void WhenTheServerRequestsToTheClientItsInformation(int index)
        {
            GetClientInformationResponse = Server
                .GetConnectedClients()
                .ElementAt(index - 1)
                .GetClientInformation();
        }

        [Then(@"The received information contains an ip to ""(.*)""")]
        public void ThenTheReceivedInformationContainsAnIpTo(string ip)
        {
            Check.That(GetClientInformationResponse.Ip).IsEqualTo(ip);
        }

        [Then(@"The received information contains a machine name to ""(.*)""")]
        public void ThenTheReceivedInformationContainsAMachineNameTo(string machineName)
        {
            Check.That(GetClientInformationResponse.MachineName).IsEqualTo(machineName);
        }
    }
}