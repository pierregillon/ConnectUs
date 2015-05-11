using System;
using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ClientSide;
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
            ClientRequestProcessor = new ClientRequestProcessor(new ModuleService());
        }

        [When(@"I process the request with the client request processor")]
        public void WhenIProcessTheRequestWithTheClientRequestProcessor()
        {
            ScenarioContext.Current.Pending();
            ClientRequestProcessor.Process("", "");
        }

        [Then(@"I get the request name ""(.*)"" and the data ""(.*)"" on the mocked client request processor")]
        public void ThenIGetTheRequestNameAndTheDataOnTheMockedClientRequestProcessor(string requestName, string data)
        {
            Check.That(((MockedClientRequestProcess) ClientRequestProcessor).GetData(requestName)).IsEqualTo(data);
        }
    }
}