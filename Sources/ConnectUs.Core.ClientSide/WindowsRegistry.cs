using System;
using System.IO;
using Microsoft.Win32;

namespace ConnectUs.Core.ClientSide
{
    public class WindowsRegistry : IRegistry
    {
        private const string StartUpRegistry = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public void AddFileToStartupRegistry(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            if (fileName == null) {
                throw new Exception("Invalid file path");
            }
            Add(StartUpRegistry, fileName, filePath);
        }
        public void RemoveFileFromStartupRegistry(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            if (fileName == null) {
                throw new Exception("Invalid file path");
            }
            Remove(StartUpRegistry, fileName);
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
        public void Remove(string subkey, string name)
        {
            using (var registryKey = Registry.LocalMachine.OpenSubKey(subkey, true)) {
                if (registryKey == null) {
                    throw new Exception(string.Format("The registry '{0}' was not found.", subkey));
                }
                if (registryKey.GetValue(name) != null) {
                    registryKey.DeleteValue(name);
                }
            }
        }
    }
}