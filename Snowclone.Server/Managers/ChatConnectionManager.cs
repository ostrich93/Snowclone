using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Snowclone.Entities;

namespace Snowclone.Managers
{
    public class ChatConnectionManager
    {
        public List<ChatConnection> ChatConnections { get; set; }
        private readonly object _lock = new object();

        public ChatConnectionManager()
        {
            ChatConnections = new List<ChatConnection>();
        }

        public void AddConnection(ChatConnection conn)
        {
            if (!ChatConnections.Contains(conn))
            {
                lock (_lock) ChatConnections.Add(conn);
            }
        }

        public void RemoveConnection(ChatConnection chatConn)
        {
            if (ChatConnections.Contains(chatConn))
                lock (_lock) ChatConnections.Remove(chatConn);
        }

        public bool ContainsClient(TcpClient client)
        {
            if (ChatConnections.Count > 0 && ChatConnections != null)
            {
                foreach (ChatConnection cc in ChatConnections)
                {
                    if (cc.Socket == client)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
