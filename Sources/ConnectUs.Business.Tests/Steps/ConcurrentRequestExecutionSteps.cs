using System;
using System.Collections.Concurrent;
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
        private List<Task> Tasks
        {
            get { return ScenarioContext.Current.Get<List<Task>>("Tasks"); }
            set { ScenarioContext.Current.Add("Tasks", value); }
        }
        private List<EchoResponse> MainThreadResponses
        {
            get { return ScenarioContext.Current.Get<List<EchoResponse>>("MainThreadResponses"); }
            set { ScenarioContext.Current.Add("MainThreadResponses", value); }
        }
        private ConcurrentDictionary<int, EchoResponse> ResponseByThread
        {
            get { return ScenarioContext.Current.Get<ConcurrentDictionary<int, EchoResponse>>("ResponseByThread"); }
            set { ScenarioContext.Current.Add("ResponseByThread", value); }
        }

        [BeforeScenario("ConcurrentRequestExecution")]
        public void BeforeScenario()
        {
            Tasks = new List<Task>();
            MainThreadResponses = new List<EchoResponse>();
            ResponseByThread = new ConcurrentDictionary<int, EchoResponse>();
        }

        [When(@"I send the request ""(.*)"" through the server request processor on the thread (.*)")]
        public void WhenISendTheRequestThroughTheServerConnectionOnTheThread(string requestName, int threadId)
        {
            Tasks.Add(Task.Factory.StartNew(() =>
            {
                var response = (EchoResponse)ServerRequestProcessor.Process(new EchoRequest());
                ResponseByThread.GetOrAdd(threadId, i => response);
            }));
        }

        [When(@"I send the request ""(.*)"" through the server request processor on main thread")]
        public void WhenISendTheRequestThroughTheServerRequestProcessorOnMainThread(string requestName)
        {
            MainThreadResponses.Add((EchoResponse)ServerRequestProcessor.Process(new EchoRequest()));
        }

        [Then(@"I get a response with the result ""(.*)"" on thread (.*)")]
        public void ThenIGetAResponseWithTheResultFromClientConnectionOnThread(string result, int threadId)
        {
            try {
                if (Tasks.Any()) {
                    Task.WaitAll(Tasks.ToArray());
                    Tasks.Clear();
                }
                Check.That(ResponseByThread[threadId].Result).IsEqualTo(result);
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        [Then(@"I get a response with the result ""(.*)"" on main thread index (.*)")]
        public void ThenIGetAResponseWithTheResultOnMainThreadIndex(string result, int index)
        {
            Check.That(MainThreadResponses.ElementAt(index).Result).IsEqualTo(result);
        }
    }

    public class EchoResponse : Response
    {
        public string Result { get; set; }
    }

    public class EchoRequest : Request
    {
        public EchoRequest() : base("Echo")
        {
        }
    }
}