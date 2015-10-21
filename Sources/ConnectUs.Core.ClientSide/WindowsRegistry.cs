using System;
using System.IO;
using Microsoft.Win32;

namespace ConnectUs.Core.ClientSide
{
    public class WindowsRegistry : IRegistry
    {
        public void AddFileToStartupRegistry(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            if (fileName == null) {
                throw new Exception("Invalid file path");
            }
            Add("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", fileName, filePath);
        }

        public void Add(string subkey, string name, string value)
        {
            using (var registryKey = Registry.LocalMachine.OpenSubKey(subkey, true)) {
                if (registryKey == null) {
                    throw new Exception("error");
                }
                if (registryKey.GetValue(name) == null) {
                    registryKey.SetValue(name, value);
                }
            }
        }
    }
}