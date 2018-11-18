using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowclone.Entities;

namespace Snowclone.Data.Repositories
{
    public class MemberRepository : Repository<Member>
    {
        private readonly NamespaceDbContext _memberEntities;

        public MemberRepository(NamespaceDbContext context) : base(context)
        {
            _memberEntities = context;
        }
    }
}
