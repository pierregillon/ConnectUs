using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConnectUs.Business.Tests.Mocks;
using ConnectUs.ServerSide;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ConcurrentRequestExecutionSteps
    {
        public IServerRequestProcessor ServerRequestProcessor
        {
            get { return ScenarioContext.Current.Get<IServerRequestProcessor>("ServerRequestProcessor"); }
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
        public string FilePath
        {
            get { return ScenarioContext.Current.Get<string>("FilePath"); }
            set { ScenarioContext.Current.Set(value, "FilePath"); }
        }

        [BeforeScenario("ConcurrentRequestExecution")]
        public void BeforeScenario()
        {
            Tasks = new List<Task>();
            MainThreadResponses = new List<EchoResponse>();
            ResponseByThread = new ConcurrentDictionary<int, EchoResponse>();
        }

        // When

        [When(@"I send an echo request with value ""(.*)"" through the server request processor on the thread (.*)")]
        public void WhenISendAnEchoRequestWithValueThroughTheServerRequestProcessorOnTheThread(int value, int threadId)
        {
            Tasks.Add(Task.Factory.StartNew(() =>
            {
                var response = ServerRequestProcessor.ProcessRequest<EchoRequest, EchoResponse>(new EchoRequest(value.ToString()));
                ResponseByThread.GetOrAdd(threadId, i => response);
            }));
        }

        [When(@"I send an echo request with value ""(.*)"" through the server request processor on main thread")]
        public void WhenISendAnEchoRequestWithValueThroughTheServerRequestProcessorOnMainThread(int value)
        {
            MainThreadResponses.Add(ServerRequestProcessor.ProcessRequest<EchoRequest, EchoResponse>(new EchoRequest(value.ToString())));
        }

        [When(@"I upload file '(.*)' to '(.*)' through the server request processor")]
        public void WhenIUploadFileToThroughTheServerRequestProcessor(string sourceFilePath, string targetDirectory)
        {
            FilePath = ServerRequestProcessor.UploadFile(sourceFilePath, targetDirectory);
        }

        // Then

        [Then(@"I get an echo response with the result ""(.*)"" on thread (.*)")]
        public void ThenIGetAnEchoResponseWithTheResultOnThread(int result, int threadId)
        {
            try {
                if (Tasks.Any()) {
                    Task.WaitAll(Tasks.ToArray());
                    Tasks.Clear();
                }
                Check.That(ResponseByThread[threadId].Result).IsEqualTo(result.ToString());
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        [Then(@"I get an echo response with the result ""(.*)"" on main thread index (.*)")]
        public void ThenIGetAnEchoResponseWithTheResultOnMainThreadIndex(int result, int index)
        {
            Check.That(MainThreadResponses.ElementAt(index).Result).IsEqualTo(result.ToString());
        }

        [Then(@"I get the file path result '(.*)'")]
        public void ThenIGetTheFilePathResult(string filePath)
        {
            var expectedFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            Check.That(FilePath).IsEqualTo(expectedFilePath);
        }
    }
}