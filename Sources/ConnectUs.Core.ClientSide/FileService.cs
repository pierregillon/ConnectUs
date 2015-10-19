using System.IO;

namespace ConnectUs.Core.ClientSide
{
    public class FileService : IFileService
    {
        public void Copy(string sourceFileName, string destinationFileName)
        {
            if (File.Exists(sourceFileName)) {
                File.Delete(sourceFileName);
            }
            File.Copy(sourceFileName, destinationFileName);
        }
    }
}