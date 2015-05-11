using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ResponseSteps
    {
        public Response Response
        {
            get { return ScenarioContext.Current.Get<Response>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }

        [Then(@"The response is a ""(.*)"" response")]
        public void ThenTheResponseIsAResponse(string responseType)
        {
            if (responseType == "GetClientInformation") {
                Check.That(Response).IsInstanceOf<GetClientInformationResponse>();
            }
            else {
                Assert.Fail("Unknown response");
            }
        }

        [Then(@"The ip of the GetClientInformation response is ""(.*)""")]
        public void ThenTheIpOfTheGetClientInformationResponseIs(string ip)
        {
            Check.That(((GetClientInformationResponse) Response).Ip).IsEqualTo(ip);
        }

        [Then(@"The machine name of the GetClientInformation response is ""(.*)""")]
        public void ThenTheMachineNameOfTheGetClientInformationResponseIs(string machineName)
        {
            Check.That(((GetClientInformationResponse)Response).MachineName).IsEqualTo(machineName);
        }
    }
}