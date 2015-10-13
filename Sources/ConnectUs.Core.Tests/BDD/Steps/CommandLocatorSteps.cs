using ConnectUs.Core.ClientSide;
using ConnectUs.Core.Connections;
using ConnectUs.Core.ModuleManagement;
using ConnectUs.Core.Tests.BDD.Mocks;
using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.BDD.Steps
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