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

namespace Snowclone.Server
{
    public class Server : IDisposable
    {
        public string IP;
        public int Port;

        private CancellationTokenSource _UserCancellationTokenSource;
        private CancellationToken _UserCancellationToken;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private ConcurrentBag<Member> memberlist
    }
}
