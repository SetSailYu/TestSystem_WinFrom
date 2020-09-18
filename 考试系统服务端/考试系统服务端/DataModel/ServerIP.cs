using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Serializable]
    public class ServerIP
    {
        public string Serverip { get => serverip; set => serverip = value; }
        public int Port { get => port; set => port = value; }

        private string serverip;
        private int port;

        public ServerIP(string ip, int port)
        {
            this.Serverip = ip;
            this.Port = port;
        }
    }
}
