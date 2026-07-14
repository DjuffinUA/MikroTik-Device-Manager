using System;
using System.Collections.Generic;
using System.Text;

namespace MikroTik_Device_Manager.models
{
    public class ConnectionInfo
    {
        public string Ip { get; set; } = "";

        public int Port { get; set; } = 22;

        public string Login { get; set; } = "";

        public string Password { get; set; } = "";

        public string Comment { get; set; } = "";

    }
}
