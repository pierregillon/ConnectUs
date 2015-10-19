using System.Reflection;

namespace ConnectUs.Core.ClientSide
{
    public class ClientEnvironment : IEnvironment
    {
        private readonly string _location;

        public ClientEnvironment()
        {
            _location = Assembly.GetExecutingAssembly().Location;
        }

        public string ApplicationPath
        {
            get { return _location; }
        }
    }
}