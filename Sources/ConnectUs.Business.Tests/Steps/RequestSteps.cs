using TechTalk.SpecFlow;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class RequestSteps
    {
        public Request Request
        {
            get { return ScenarioContext.Current.Get<Request>("Request"); }
            set { ScenarioContext.Current.Add("Request", value); }
        }

        [Given(@"A request with the name ""(.*)""")]
        public void GivenARequestWithTheName(string requestName)
        {
            Request = new Request{Name = requestName};
        }

        [Given(@"The request has a parameter ""(.*)"" with the value ""(.*)""")]
        public void GivenTheRequestHasAParameterWithTheValue(string name, string value)
        {
            Request.Parameters.Add(new RequestParameter(name, value));
        }
    }

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