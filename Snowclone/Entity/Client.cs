using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Snowclone.Entities
{
    public class Client
    {
        public Member MemberInfo { get; private set; }

        //private User _userInfo;

        public int Port { get; set; } //source Port

        public IPAddress IPAddress { get; set; } //source IP

        public TcpClient tcpClient { get; set; }

        public bool Connected { get; set; }

        public bool LoggedIn { get; set; }

        public void SetMemberData(Member info)
        {
            MemberInfo = info;
        }

        public void SetServer(IPAddress ip, int port)
        {
            IPAddress = ip;
            Port = port;
        }

        public void connect()
        {
            try
            {
                tcpClient = new TcpClient(IPAddress.ToString(), Port);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Connection refused by the server: " + e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
