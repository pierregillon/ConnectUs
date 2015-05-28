using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConnectUs.ServerSide.CommandLines
{
    public class UploadCommandLine : ICommandLine
    {
        public string Name { get { return "upload"; } }
        public string ExecuteCommand(Client client, IEnumerable<string> parameters)
        {
            if (parameters.Any() == false) {
                return "Missing parameters";
            }
            var filePath = parameters.First();
            client.Upload(filePath);
            return string.Format("Le fichier '{0}' a bien été uploadé.", Path.GetFileName(filePath));
        }
    }
}
