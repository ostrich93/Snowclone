using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowclone.Entities;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;

namespace Snowclone.Utilities
{
    public class DatabaseConfig
    {
        public string DatabaseUser { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseIp { get; set; }
        public int DatabasePort { get; set; }
        public int ConnectionTimeout { get; set; }
        public SqlProtocol SqlProtocol { get; set; }
    }

    public class ParentConfig
    {
        public string TenantServer { get; set; }
        public string TenantDatabase { get; set; }
    }

    public class TenantConfig
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseServerName { get; set; }
    }

    public class TenantServerConfig
    {
        public string TenantServer { get; set; }
        public string TenantDatabase { get; set; }
    }
}
