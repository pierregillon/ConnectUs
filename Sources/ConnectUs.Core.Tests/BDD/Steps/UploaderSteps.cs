using System.Threading.Tasks;
using ConnectUs.Core.Connections;
using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.BDD.Steps
{
    [Binding]
    public class UploaderSteps
    {
        public Uploader Uploader
        {
            get { return ScenarioContext.Current.Get<Uploader>("Uploader"); }
            set { ScenarioContext.Current.Set(value, "Uploader"); }
        }
        public IConnection ServerConnection
        {
            get { return ScenarioContext.Current.Get<IConnection>("ServerConnection"); }
            set { ScenarioContext.Current.Set(value, "ServerConnection"); }
        }
        public Task UploadTask
        {
            get { return ScenarioContext.Current.Get<Task>("UploadTask"); }
            set { ScenarioContext.Current.Set(value, "UploadTask"); }
        }

        [Given(@"An uploader with the server connection")]
        public void GivenAnUploaderWithTheServerConnection()
        {
            Uploader = new Uploader(ServerConnection);
        }

        [When(@"I start the upload to the file ""(.*)""")]
        public void WhenIStartTheUploadToTheFile(string filePath)
        {
            UploadTask = Task.Run(() => Uploader.Upload(filePath));
        }
    }
}