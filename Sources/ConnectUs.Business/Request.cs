namespace ConnectUs.Business
{
    public class Request : RequestBase
    {
        public override string Name { get { return GetType().Name; } set { } }
    }
}