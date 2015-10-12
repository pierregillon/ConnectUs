using System.IO;
using System.Threading.Tasks;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.Steps
{
    [Binding]
    public class TaskSynchroniserSteps
    {
        public Task DownloadTask
        {
            get { return ScenarioContext.Current.Get<Task>("DownloadTask"); }
            set { ScenarioContext.Current.Set(value, "DownloadTask"); }
        }
        public Task UploadTask
        {
            get { return ScenarioContext.Current.Get<Task>("UploadTask"); }
            set { ScenarioContext.Current.Set(value, "UploadTask"); }
        }

        [When(@"I wait the end of the file exchange")]
        public void WhenIWaitTheEndOfTheFileExchange()
        {
            Task.WaitAll(new[] {DownloadTask, UploadTask});
        }

        [Then(@"The ""(.*)"" file and the ""(.*)"" file are equals")]
        public void ThenTheFileAndTheFileAreEquals(string sourceFilePath, string targetFilePath)
        {
            Check.That(File.ReadAllText(sourceFilePath)).IsEqualTo(File.ReadAllText(targetFilePath));
        }
    }
}