using System;
using ConnectUs.Business;

namespace ConnectUs.ServerSide
{
    public class PackageClient
    {
        private readonly Client _client;

        public PackageClient(Client client)
        {
            _client = client;
        }

        public void InstallPackage(string packageName, Version version)
        {
            var request = new Request("install-package");
            //request.Parameters.Add(new Business.RequestParameter("PackageName", packageName));
            //request.Parameters.Add(new Business.RequestParameter("Version", version.ToString()));
            _client.Execute(request);
        }
    }
}