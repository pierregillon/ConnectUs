using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConnectUs.Core.ServerSide.Clients;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public class UploadCommandLine : ICommandLine
    {
        public string Name
        {
            get { return "upload"; }
        }
        public string ExecuteCommand(IRemoteClient remoteClient, IEnumerable<string> parameters)
        {
            if (!parameters.Any()) {
                return "Missing parameters";
            }
            var sourceFilePath = parameters.First();
            var targetDirectory = parameters.Count() == 2 ? parameters.Last() : string.Empty;
            var filePath = remoteClient.UploadFile(sourceFilePath, targetDirectory);
            return string.Format("Le fichier '{0}' a bien été uploadé à l'emplacement '{1}'.", Path.GetFileName(sourceFilePath), filePath);
        }
    }
}