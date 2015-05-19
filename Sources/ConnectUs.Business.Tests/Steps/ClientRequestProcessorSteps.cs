using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
using Newtonsoft.Json;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ClientRequestProcessorSteps
    {
        public IClientRequestProcessor ClientRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IClientRequestProcessor>("ClientRequestProcessor"); }
            set { ScenarioContext.Current.Add("ClientRequestProcessor", value); }
        }
        public ICommandLocator CommandLocator
        {
            get { return ScenarioContext.Current.Get<ICommandLocator>("CommandLocator"); }
            set { ScenarioContext.Current.Add("CommandLocator", value); }
        }
        public string Result
        {
            get { return ScenarioContext.Current.Get<string>("Result"); }
            set { ScenarioContext.Current.Add("Result", value); }
        }

        [Given(@"A mocked client request processor")]
        public void GivenAMockedClientRequestProcessor()
        {
            ClientRequestProcessor = new MockedClientRequestProcess();
        }

        [Given(@"A mocked client request processor that returns error ""(.*)""")]
        public void GivenAMockedClientRequestProcessorThatReturnsError(string message)
        {
            ClientRequestProcessor = new MockedErrorClientRequestProcess(message);
        }

        [Given(@"A client request processor")]
        public void GivenAClientRequestProcessor()
        {
            ClientRequestProcessor = new ClientRequestProcessor(CommandLocator, new JsonRequestParser());
        }

        [When(@"I process the request ""(.*)"" with the data ""(.*)""")]
        public void WhenIProcessTheRequestWithTheData(string requestName, string data)
        {
            Result = ClientRequestProcessor.Process(requestName, data);
        }

        [Then(@"I get the request name ""(.*)"" and the data ""(.*)"" on the mocked client request processor")]
        public void ThenIGetTheRequestNameAndTheDataOnTheMockedClientRequestProcessor(string requestName, string data)
        {
            Check.That(((MockedClientRequestProcess) ClientRequestProcessor).GetData(requestName)).IsEqualTo(data);
        }

        [Then(@"I get the response ""(.*)""")]
        public void ThenIGetTheResponse(string data)
        {
            Check.That(Result).IsEqualTo(data);
        }

        [Then(@"I get a process exception ""(.*)""")]
        public void ThenIGetAProcessException(string text)
        {
            Check.That(Result).IsEqualTo(text);
        }
    }
}