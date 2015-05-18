using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class CommandLocatorSteps
    {
        public ICommandLocator CommandLocator
        {
            get { return ScenarioContext.Current.Get<ICommandLocator>("CommandLocator"); }
            set { ScenarioContext.Current.Add("CommandLocator", value); }
        }

        [Given(@"A mocked command locator")]
        public void GivenAMockedCommandLocator()
        {
            CommandLocator = new MockCommandLocator();
        }
    }
}