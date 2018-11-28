using Snowclone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowclone.Entities
{
    public class Tenant : BaseEntity
    {
        public int TenantId { get; set; }

        public string TenantName { get; set; }

        public string ServerName { get; set; }

        public string ConnectionString { get; set; }

        public string IPAddress { get; set; }

        public int Port { get; set; }

        public List<TenantUser> TenantUsers { get; set; }

        //public List<User> BanList { get; set; }
    }
}
