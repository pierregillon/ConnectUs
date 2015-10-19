using System.IO;

namespace ConnectUs.Core.ClientSide
{
    public class FileService : IFileService
    {
        public void Copy(string sourceFileName, string destinationFileName)
        {
            File.Copy(sourceFileName, destinationFileName);
        }
    }
}