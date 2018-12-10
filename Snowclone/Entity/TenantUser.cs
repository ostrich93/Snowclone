using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowclone.Data;

namespace Snowclone.Entities
{
    public class TenantUser : BaseEntity
    {
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
