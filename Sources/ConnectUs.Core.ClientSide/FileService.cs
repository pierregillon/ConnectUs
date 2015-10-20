using System.IO;

namespace ConnectUs.Core.ClientSide
{
    public class FileService : IFileService
    {
        public void Copy(string sourceFileName, string destinationFileName)
        {
            if (File.Exists(destinationFileName)) {
                File.Delete(destinationFileName);
            }
            File.Copy(sourceFileName, destinationFileName);
        }
    }
}