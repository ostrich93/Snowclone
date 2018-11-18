using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowclone.Entities;

namespace Snowclone.Data.Repositories
{
    public class MessageRepository : Repository<Message>
    {
        private readonly NamespaceDbContext _messageEntities;

        public MessageRepository(NamespaceDbContext context) : base(context)
        {
            _messageEntities = context;
        }
    }
}
