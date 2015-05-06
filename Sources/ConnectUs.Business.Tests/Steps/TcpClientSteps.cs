using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class TcpClientSteps
    {
        public TcpClient TcpClient
        {
            get { return ScenarioContext.Current.Get<TcpClient>("TcpClient"); }
            set { ScenarioContext.Current.Add("TcpClient", value); }
        }

        [Given(@"A tcp client")]
        public void GivenATcpClient()
        {
            TcpClient = new TcpClient();
        }

        [When(@"The tcp client connect to the host '(.*)' and the port (.*)")]
        public void WhenTheTcpClientConnectToTheHostAndThePort(string ip, int port)
        {
            TcpClient.Connect(ip, port);
            Thread.Sleep(50);
        }

        [Then(@"the tcp client received the request '(.*)'")]
        public void ThenTheTcpClientReceivedTheRequest(string requestName)
        {
            var buffer = new byte[33];
            var bytesReadCount = TcpClient.GetStream().Read(buffer, 0, buffer.Length);
            Check.That(bytesReadCount).IsEqualTo(33);
            var encoding = new UTF8Encoding();
            var request = JsonConvert.DeserializeObject<Request>(encoding.GetString(buffer));
            Check.That(request.Name).IsEqualTo(requestName);
        }
    }
}