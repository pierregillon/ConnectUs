using ConnectUs.Business.Connections;
using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
using ConnectUs.ClientSide.ModuleManagement;
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
        public ModuleManager ModuleManager
        {
            get { return ScenarioContext.Current.Get<ModuleManager>("ModuleManager"); }
            set { ScenarioContext.Current.Set(value, "ModuleManager"); }
        }
        public IConnection ClientConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ClientConnection"); }
            set { ScenarioContext.Current.Set(value, "ClientConnection"); }
        }

        [Given(@"A mocked command locator")]
        public void GivenAMockedCommandLocator()
        {
            CommandLocator = new MockCommandLocator();
        }

        [Given(@"A command locator")]
        public void GivenACommandLocator()
        {
            CommandLocator = new CommandLocator(ModuleManager, new ClientInformation{CurrentConnection = ClientConnection});
        }
    }
}