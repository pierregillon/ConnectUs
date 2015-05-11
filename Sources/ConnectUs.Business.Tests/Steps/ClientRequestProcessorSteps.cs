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
        public IModuleService ModuleService
        {
            get { return ScenarioContext.Current.Get<IModuleService>("ModuleService"); }
            set { ScenarioContext.Current.Add("ModuleService", value); }
        }
        public object Result
        {
            get { return ScenarioContext.Current.Get<object>("Result"); }
            set { ScenarioContext.Current.Add("Result", value); }
        }
        public ProcessException ProcessError
        {
            get { return ScenarioContext.Current.Get<ProcessException>("ProcessError"); }
            set { ScenarioContext.Current.Add("ProcessError", value); }
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
            ClientRequestProcessor = new ClientRequestProcessor(ModuleService);
        }

        [When(@"I process the request ""(.*)"" with the data ""(.*)""")]
        public void WhenIProcessTheRequestWithTheData(string requestName, string data)
        {
            try {
                Result = ClientRequestProcessor.Process(requestName, data);
            }
            catch (ProcessException ex) {
                ProcessError = ex;
            }
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
        public void ThenIGetAProcessException(string message)
        {
            Check.That(ProcessError).IsNotNull();
            Check.That(ProcessError.Message).IsEqualTo(message);
        }
    }
}