using System;
using Microsoft.Win32;

namespace ConnectUs.Core.ClientSide
{
    public class WindowsRegistry : IRegistry
    {
        public void AddInStartupRegistry(string value)
        {
            Add("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "ApplicationName", value);
        }
        public void Add(string subkey, string name, string value)
        {
            using (var registryKey = Registry.CurrentUser.OpenSubKey(subkey, true)) {
                if (registryKey == null) {
                    throw new Exception("error");
                }
                registryKey.SetValue(name, value);
            }
        }
    }
}