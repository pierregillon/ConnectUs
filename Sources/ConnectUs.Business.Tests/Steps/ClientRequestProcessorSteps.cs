using ConnectUs.Business.Tests.Mocks;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ClientRequestProcessorSteps
    {
        public MockedClientRequestProcess MockedClientRequestProcess
        {
            get { return ScenarioContext.Current.Get<MockedClientRequestProcess>("MockedClientRequestProcess"); }
            set { ScenarioContext.Current.Add("MockedClientRequestProcess", value); }
        }

        [Given(@"A mocked client request processor")]
        public void GivenAMockedClientRequestProcessor()
        {
            MockedClientRequestProcess = new MockedClientRequestProcess();
        }

        [Given(@"A mocked client request processor that returns error ""(.*)""")]
        public void GivenAMockedClientRequestProcessorThatReturnsError(string message)
        {
            MockedClientRequestProcess = new MockedErrorClientRequestProcess(message);
        }

        [Then(@"I get the request name ""(.*)"" and the data ""(.*)"" on the mocked client request processor")]
        public void ThenIGetTheRequestNameAndTheDataOnTheMockedClientRequestProcessor(string requestName, string data)
        {
            Check.That(MockedClientRequestProcess.GetData(requestName)).IsEqualTo(data);
        }

    }
}