using System;
using System.IO;

namespace ConnectUs.Core.ClientSide
{
    public class FileService : IFileService
    {
        public void Copy(string sourceFileName, string destinationFileName)
        {
            if (File.Exists(destinationFileName) == false) {
                File.Copy(sourceFileName, destinationFileName);
            }
        }
        public string GenerateRandomFileName()
        {
            try {
                return GetFileNameFromMachine();
            }
            catch (Exception) {
                return GetRandomFileName();
            }
        }

        private static string GetRandomFileName()
        {
            return Path.GetRandomFileName().Replace(".", "").Substring(0, 10);
        }
        private static string GetFileNameFromMachine()
        {
            var files = Directory.GetFiles(@"C:\Windows\System32");
            var random = new Random((int) DateTime.Now.Ticks);
            var index = random.Next(files.Length);
            var fileName = files[index];
            var extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension) == false) {
                fileName = fileName.Replace(extension, string.Empty);
            }
            return fileName + random.Next(10, 30);
        }
    }
}