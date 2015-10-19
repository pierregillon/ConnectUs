using System;

namespace ConnectUs.Core.ClientSide
{
    public class ClientEnvironment : IEnvironment
    {
        public string CurrentParentFolder
        {
            get { return Environment.CurrentDirectory; }
        }
    }
}