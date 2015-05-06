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

        [Given(@"A response with the content ""(.*)""")]
        public void GivenAResponseWithTheContent(string content)
        {
            Response = new Response{Content = content};
        }

    }
}