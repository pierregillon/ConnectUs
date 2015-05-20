using System.IO;
using System.Threading.Tasks;
using ConnectUs.Business.Connections;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class DownloaderSteps
    {
        public Downloader Downloader
        {
            get { return ScenarioContext.Current.Get<Downloader>("Downloader"); }
            set { ScenarioContext.Current.Set(value, "Downloader"); }
        }
        public IConnection ClientConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ClientConnection"); }
            set { ScenarioContext.Current.Set(value, "ClientConnection"); }
        }
        public Task DownloadTask
        {
            get { return ScenarioContext.Current.Get<Task>("DownloadTask"); }
            set { ScenarioContext.Current.Set(value, "DownloadTask"); }
        }

        [Given(@"A downloader with the client connection")]
        public void GivenADownloaderWithTheClientConnection()
        {
            Downloader = new Downloader(ClientConnection);
        }

        [When(@"I start the downloader to save the file ""(.*)""")]
        public void WhenIStartTheDownloaderToSaveTheFile(string filePath)
        {
            DownloadTask = Task.Run(() => Downloader.Download(filePath));
        }

        [Then(@"The downloader received the file ""(.*)""")]
        public void ThenTheDownloaderReceivedTheFile(string filePath)
        {
            Check.That(File.Exists(filePath)).IsTrue();
        }
    }
}