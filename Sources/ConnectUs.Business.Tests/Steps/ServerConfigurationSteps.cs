using ConnectUs.ServerSide;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ServerConfigurationSteps
    {
        public ServerConfiguration ServerConfiguration
        {
            get { return ScenarioContext.Current.Get<ServerConfiguration>("ServerConfiguration"); }
            set { ScenarioContext.Current.Add("ServerConfiguration", value); }
        }

        [Given(@"A server configuration with a port set to (.*)")]
        public void GivenAServerConfigurationWithAPortSetTo(int port)
        {
            ServerConfiguration = new ServerConfiguration
            {
                Port = port
            };
        }
    }
}