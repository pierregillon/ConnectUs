using ConnectUs.Modules.Integrated.GetClientInformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.BDD.Steps
{
    [Binding]
    public class ResponseSteps
    {
        public object Response
        {
            get { return ScenarioContext.Current.Get<object>("Response"); }
            set { ScenarioContext.Current.Add("Response", value); }
        }

        [Given(@"A response with the name ""(.*)""")]
        public void GivenAResponseWithTheName(string p0)
        {
            Response = new GetClientInformationResponse();
        }

        [Given(@"The GetClientInformation response has the ip ""(.*)""")]
        public void GivenTheGetClientInformationResponseHasTheIp(string ip)
        {
            ((GetClientInformationResponse) Response).Ip = ip;
        }

        [Given(@"The GetClientInformation response has the machine name ""(.*)""")]
        public void GivenTheGetClientInformationResponseHasTheMachineName(string machineName)
        {
            ((GetClientInformationResponse)Response).MachineName = machineName;
        }

        [Then(@"The response is a ""(.*)"" response")]
        public void ThenTheResponseIsAResponse(string responseType)
        {
            if (responseType == "GetClientInformation")
            {
                Check.That(Response).IsInstanceOf<GetClientInformationResponse>();
            }
            else
            {
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
            Check.That(((GetClientInformationResponse) Response).MachineName).IsEqualTo(machineName);
        }
    }
}