using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Snowclone.Services;
using Snowclone.Entities;
using System.Diagnostics;

namespace Snowclone.Utilities
{
    public class Sharding
    {
        private static TenantServices _tenantRepo;
        private static UniversalServices _universalRepo;

        public ShardMapManager ShardMapManager { get; }

        public static ListShardMap<int> ShardMap { get; set; }

        public Sharding(string generalDbName, string connectionString, UniversalServices universalServices, TenantServices tenantServices)
        {
            try
            {
                _tenantRepo = tenantServices;
                _universalRepo = universalServices;

                ShardMapManager shardMapManager;

                ListShardMap<int> sm;
            }

            catch (Exception exception)
            {
                Trace.TraceError(exception.Message, "Error in sharding utilties initialization.");
            }
        }

        public static Shard CreateNewShard(string tenantName, string serverName, int serverPort)
        {
            Shard shard;
            try
            {
                ShardLocation shardLocation = new ShardLocation(serverName, tenantName, SqlProtocol.Tcp, serverPort);
                if (!ShardMap.TryGetShard(shardLocation, out shard))
                {
                    shard = ShardMap.CreateShard(shardLocation);
                }
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message, "Error in creating new shard.");
                shard = null;
            }
            return shard;
        }

        public static async Task<bool> RegisterNewShard(int tenantId, string tenantName, string serverName, Shard shard)
        {
            try
            {
                PointMapping<int> pointMapping;
                if (!ShardMap.TryGetMappingForKey(tenantId, out pointMapping))
                {
                    var pMapping = ShardMap.CreatePointMapping(tenantId, shard);

                    var tenant = new Tenant
                    {
                        TenantId = tenantId,
                        TenantName = tenantName,
                        ServerName = serverName
                    };

                    await _universalRepo.AddTenant(tenant);
                }
                return true;
            }
            catch(Exception exception)
            {
                Trace.TraceError(exception.Message, "Error in registering new database shard");
                return false;
            }
        }
    }
}
