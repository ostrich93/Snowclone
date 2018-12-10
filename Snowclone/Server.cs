using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Threading;
using Snowclone.Entities;
using Snowclone.Services;
using Snowclone.Utilities;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Snowclone.Managers;

namespace Snowclone
{
    public class Server : IDisposable
    {

        public IPAddress IP;
        public int Port;

        private ChatConnectionManager chatConnectionManager = new ChatConnectionManager();

        TcpListener Listener;

        private readonly object _ClientsLock = new object();
        private readonly object _MemberInfoLock = new object();

        //private TenantServices _TenantServices;

        public bool isRunning { get; set; }

        //private CancellationTokenSource _UserCancellationTokenSource;
        //private CancellationToken _UserCancellationToken;

        public void StartServer(IPAddress address, int port)
        {
            IP = address;
            Port = port;
            isRunning = false;
            try
            {
                Listener = new TcpListener(address, port);
                Listener.Start();
                isRunning = true;
            }
            catch (SocketException e)
            {
                Console.WriteLine("Cannot start the server: " + e.Message);
            }
        }

        public void StopServer()
        {
            Console.WriteLine("Stopping server");
            this.isRunning = false;
            Listener.Stop();
        }

        public void Listen()
        {
            while (isRunning)
            {
                try
                {
                    Console.WriteLine("Waiting for new connection...");
                    TcpClient client = Listener.AcceptTcpClient();
                    if (!chatConnectionManager.ContainsClient(client))
                    {
                        ChatConnection chatConnection = new ChatConnection();
                        chatConnection.Socket = client;
                        Member memInfo;
                        lock (_MemberInfoLock)
                        {
                            memInfo = GetMember(client.Client);
                        }
                        if (memInfo != null)
                        {
                            chatConnection.MemberInfo = memInfo;
                            chatConnectionManager.AddConnection(chatConnection);

                            Console.WriteLine("New client: " + chatConnection.ClientId);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Client is already connected");
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("The Listener thread is closed");
                }
            }
        }

        private void CheckIncomingMessages()
        {
            while (isRunning)
            {
                try
                {
                    lock (_ClientsLock) {
                        foreach (ChatConnection cc in chatConnectionManager.ChatConnections)
                        {
                            if (cc != null && cc.Socket.GetStream().DataAvailable)
                            {
                                Message msg = GetMessage(cc.Socket.Client);
                                if (msg != null)
                                {
                                    //Thread broadcastThread = new Thread(() => BroadcastMessage(cc, msg));
                                    //broadcastThread.Start();
                                }
                            }
                        }
                    }
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public Member GetMember(Socket socket)
        {
            try
            {
                NetworkStream strm = new NetworkStream(socket);
                IFormatter formatter = new BinaryFormatter();
                Member member = (Member)formatter.Deserialize(strm);
                return member;
            }
            catch (EndOfStreamException e)
            {

            }
            catch (IOException e)
            {

            }
            catch (InvalidOperationException e)
            {

            }
            return null;
        }

        public Message GetMessage(Socket socket)
        {
            try
            {
                NetworkStream strm = new NetworkStream(socket);
                IFormatter formatter = new BinaryFormatter();
                Message msg = (Message)formatter.Deserialize(strm);
                return msg;
            }
            catch (EndOfStreamException e)
            {

            }
            catch (IOException e)
            {

            }
            catch (InvalidOperationException e)
            {

            }
            return null;
        }

        public void SendMessage(Message msg, Socket socket)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                NetworkStream stream = new NetworkStream(socket);
                formatter.Serialize(stream, socket);
            }
            catch (EndOfStreamException e)
            {

            }
            catch (IOException e)
            {

            }
            catch (InvalidOperationException e)
            {

            }
        }

        public void BroadcastMessage(ChatConnection cc, Message msg)
        {
            if (msg != null && String.IsNullOrEmpty(msg.Content))
            {
                foreach (ChatConnection chatConn in chatConnectionManager.ChatConnections)
                {
                    if (chatConn.MemberInfo != null && chatConn != cc)
                    {
                        SendMessage(msg, chatConn.Socket.Client);
                    }
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
