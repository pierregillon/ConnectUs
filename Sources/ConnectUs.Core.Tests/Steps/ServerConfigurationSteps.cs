using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.Steps
{
    [Binding]
    public class ServerConfigurationSteps
    {
        public int Port
        {
            get { return ScenarioContext.Current.Get<int>("Port"); }
            set { ScenarioContext.Current.Add("Port", value); }
        }

        [Given(@"A server configuration with a port set to (.*)")]
        public void GivenAServerConfigurationWithAPortSetTo(int port)
        {
            Port = port;
        }
    }
}