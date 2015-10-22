using System;
using System.IO;
using System.Linq;

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
            return GetFileNameFromMachine() ?? GetRandomFileName();
        }
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        private static string GetRandomFileName()
        {
            return Path.GetRandomFileName().Replace(".", "").Substring(0, 10) + ".exe";
        }
        private static string GetFileNameFromMachine()
        {
            var files = Directory.GetFiles(@"C:\Windows\System32");
            var random = new Random((int) DateTime.Now.Ticks);
            var index = random.Next(files.Length);
            
            for (var i = 0; i < 5; i++) {
                var fileName = Path.GetFileName(files[index]);
                var extension = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(extension) == false) {
                    fileName = fileName.Replace(extension, string.Empty);
                }
                fileName += random.Next(10, 30) + ".exe";
                if (files.All(x => Path.GetFileName(x) != fileName)) {
                    return fileName;
                }
            }

            return null;
        }
    }
}