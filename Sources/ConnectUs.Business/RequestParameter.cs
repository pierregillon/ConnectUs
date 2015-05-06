namespace ConnectUs.Business
{
    public class RequestParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public RequestParameter()
        {
            
        }
        public RequestParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}