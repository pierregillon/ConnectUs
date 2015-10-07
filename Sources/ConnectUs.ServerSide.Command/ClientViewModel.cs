using System.Diagnostics;

namespace ConnectUs.ServerSide.Command
{
    public class ClientViewModel
    {
        public string Ip { get; private set; }
        public string MachineName { get; private set; }
        public int Latency { get; private set; }
        public Client Client { get; private set; }

        public ClientViewModel(Client client)
        {
            Client = client;

            var information = Client.GetClientInformation();
            Ip = information.Ip;
            MachineName = information.MachineName;
        }

        public bool Match(Client client)
        {
            return Client == client;
        }
        public void Ping()
        {
            try {
                var watch = new Stopwatch();
                watch.Start();
                Client.Ping();
                watch.Stop();
                Latency = (int) watch.ElapsedMilliseconds;
            }
            catch (ClientException) {
                Client.CloseConnection();
            }
        }
    }
}