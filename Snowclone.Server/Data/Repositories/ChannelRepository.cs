using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowclone.Entities;

namespace Snowclone.Data.Repositories
{
    public class ChannelRepository : Repository<Channel>
    {
        private readonly NamespaceDbContext _serverChannelEntities;

        public ChannelRepository(NamespaceDbContext context) : base(context)
        {
            _serverChannelEntities = context;
        }
    }
}
