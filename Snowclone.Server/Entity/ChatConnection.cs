using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Snowclone.Entities
{
    public delegate void ProcessMessage(Message msg);

    public class ChatConnection
    {
        public Guid ClientId { get; set; }
        public TcpClient Socket { get; set; }
        public Member MemberInfo { get; set; }

        public ChatConnection()
        {
            ClientId = Guid.NewGuid();
            Socket = null;
            MemberInfo = null;
        }

        public void SendMessage(Message message)
        {

        }

        private void WriteMessage()
        {

        }
    }
}
