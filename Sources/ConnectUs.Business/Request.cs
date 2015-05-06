using System.Collections.Generic;

namespace ConnectUs.Business
{
    public class Request
    {
        public string Name { get; set; }
        public List<RequestParameter> Parameters { get; set; }

        public Request()
        {
            Parameters = new List<RequestParameter>();
        }
    }
}