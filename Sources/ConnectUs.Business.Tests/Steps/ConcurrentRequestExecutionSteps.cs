using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ConcurrentRequestExecutionSteps
    {
        public IRequestProcessor ServerRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IRequestProcessor>("ServerRequestProcessor"); }
            set { ScenarioContext.Current.Add("ServerRequestProcessor", value); }
        }
        private readonly Dictionary<int, Response> _responseByThread = new Dictionary<int, Response>();
        private readonly List<Response> _mainThreadResponses = new List<Response>();
        private readonly List<Task> _tasks = new List<Task>();

        [When(@"I send the request ""(.*)"" through the server request processor on the thread (.*)")]
        public void WhenISendTheRequestThroughTheServerConnectionOnTheThread(string requestName, int threadId)
        {
            _tasks.Add(Task.Factory.StartNew(() =>
            {
                var response = ServerRequestProcessor.Process(new Request {Name = requestName});
                _responseByThread.Add(threadId, response);
            }));
        }

        [When(@"I send the request ""(.*)"" through the server request processor on main thread")]
        public void WhenISendTheRequestThroughTheServerRequestProcessorOnMainThread(string requestName)
        {
            _mainThreadResponses.Add(ServerRequestProcessor.Process(new Request {Name = requestName}));
        }

        [Then(@"I get a response with the result ""(.*)"" on thread (.*)")]
        public void ThenIGetAResponseWithTheResultFromClientConnectionOnThread(string result, int threadId)
        {
            if (_tasks.Any()) {
                Task.WaitAll(_tasks.ToArray());
                _tasks.Clear();
            }
            Check.That(_responseByThread[threadId].Result).IsEqualTo(result);
        }

        [Then(@"I get a response with the result ""(.*)"" on main thread index (.*)")]
        public void ThenIGetAResponseWithTheResultOnMainThreadIndex(string result, int index)
        {
            Check.That(_mainThreadResponses.ElementAt(index).Result).IsEqualTo(result);
        }
    }
}