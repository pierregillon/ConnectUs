using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ModuleServiceSteps
    {
        public IModuleService ModuleService
        {
            get { return ScenarioContext.Current.Get<IModuleService>("ModuleService"); }
            set { ScenarioContext.Current.Add("ModuleService", value); }
        }

        [Given(@"A mocked module service")]
        public void GivenAMockedModuleService()
        {
            ModuleService = new MockModuleService();
        }
    }
}