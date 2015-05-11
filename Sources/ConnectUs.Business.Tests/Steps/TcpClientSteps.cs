using System.Net.Sockets;
using System.Threading;
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

        [Then(@"the tcp client received the request")]
        public void ThenTheTcpClientReceivedTheRequest()
        {
            var buffer = new byte[33];
            var bytesReadCount = TcpClient.GetStream().Read(buffer, 0, buffer.Length);
            Check.That(bytesReadCount).IsNotEqualTo(0);
        }
    }
}