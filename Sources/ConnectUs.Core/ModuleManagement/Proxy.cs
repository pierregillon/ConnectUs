using System;
using System.Reflection;

namespace ConnectUs.Core.ModuleManagement
{
    public class Proxy : MarshalByRefObject
    {
        public Assembly LoadAssembly(string assemblyPath)
        {
            return Assembly.LoadFile(assemblyPath);
        }
    }
}