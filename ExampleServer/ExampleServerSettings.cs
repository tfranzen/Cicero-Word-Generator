using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;

namespace Virgil
{
    public class ExampleServerSettings : ServerSettingsInterface
    {
        private string serverName = "Logger";

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }
    }
}
