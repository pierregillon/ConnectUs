using System.Text;
using ConnectUs.Core.ClientSide;
using ConnectUs.Core.Connections;
using ConnectUs.Core.Tests.BDD.Mocks;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.BDD.Steps
{
    [Binding]
    public class ClientRequestProcessorSteps
    {
        public IConnection ClientConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ClientConnection"); }
            set { ScenarioContext.Current.Add("ClientConnection", value); }
        }
        public IContinuousRequestProcessor ContinuousRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IContinuousRequestProcessor>("ContinuousRequestProcessor"); }
            set { ScenarioContext.Current.Set(value, "ContinuousRequestProcessor"); }
        }
        public IClientRequestProcessor ClientRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IClientRequestProcessor>("ClientRequestProcessor"); }
            set { ScenarioContext.Current.Set(value, "ClientRequestProcessor"); }
        }
        public ICommandLocator CommandLocator
        {
            get { return ScenarioContext.Current.Get<ICommandLocator>("CommandLocator"); }
            set { ScenarioContext.Current.Set(value, "CommandLocator"); }
        }
        public IClientRequestHandler ClientRequestHandler
        {
            get { return ScenarioContext.Current.Get<IClientRequestHandler>("ClientRequestHandler"); }
            set { ScenarioContext.Current.Set(value, "ClientRequestHandler"); }
        }
        public string Result
        {
            get { return ScenarioContext.Current.Get<string>("Result"); }
            set { ScenarioContext.Current.Set(value, "Result"); }
        }

        // Given

        [Given(@"A mocked client request processor")]
        public void GivenAMockedClientRequestProcessor()
        {
            ClientRequestProcessor = new MockedClientRequestProcess();
        }

        [Given(@"A mocked client request processor that returns echo")]
        public void GivenAMockedClientRequestProcessorThatReturnsEcho()
        {
            ClientRequestProcessor = new MockedEchoClientRequestProcessor();
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

        [Given(@"A continuous client request processor")]
        public void GivenAContinuousClientRequestProcessor()
        {
            ContinuousRequestProcessor = new ContinuousRequestProcessor(ClientRequestHandler);
        }

        // When

        [When(@"I process the request ""(.*)""")]
        public void WhenIProcessTheRequestWithTheData(string json)
        {
            var encoding = new UTF8Encoding();
            var data = encoding.GetBytes(json);
            var bytes = ClientRequestProcessor.Process(data);
            Result = encoding.GetString(bytes);
        }

        [When(@"I start the continous client request process to process incoming request")]
        public void WhenIStartTheContinousClientRequestProcessToProcessIncomingRequest()
        {
            ContinuousRequestProcessor.StartProcessingRequestFromConnection(ClientConnection);
        }

        // Then

        [Then(@"I get the request name ""(.*)"" and the data ""(.*)"" on the mocked client request processor")]
        public void ThenIGetTheRequestNameAndTheDataOnTheMockedClientRequestProcessor(string requestName, string data)
        {
            Check.That(((MockedClientRequestProcess) ClientRequestProcessor).GetData()).IsEqualTo(data);
        }

        [Then(@"I get the response ""(.*)""")]
        public void ThenIGetTheResponse(string data)
        {
            Check.That(Result).IsEqualTo(data);
        }

        [Then(@"I get the message ""(.*)""")]
        public void ThenIGetAProcessException(string text)
        {
            Check.That(Result).IsEqualTo(text);
        }

        // Clear

        [AfterScenario("ConcurrentRequestExecution")]
        public void AfterScenario()
        {
            ContinuousRequestProcessor.StopProcessingRequestFromConnection();
        }
    }
}