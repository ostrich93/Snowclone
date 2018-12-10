using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowclone.Entities;
using Microsoft.EntityFrameworkCore;

namespace Snowclone.Services
{
    public class UniversalServices
    {
        private readonly GeneralDbContext _generalDbContext;

        public UniversalServices(GeneralDbContext context)
        {
            _generalDbContext = context;
        }

        public async Task<List<Tenant>> GetTenants()
        {
            var tenantList = await _generalDbContext.Tenants.ToListAsync();
            return tenantList.Count > 0 ? tenantList : null;
        }

        public async Task<Tenant> GetTenant(int tenantId)
        {
            var tenant = await _generalDbContext.Tenants.Where(t => t.TenantId == tenantId).FirstOrDefaultAsync();
            return tenant;
        }

        public async Task<Tenant> GetTenantByName(string tenantName)
        {
            var tenant = await _generalDbContext.Tenants.Where(t => t.TenantName == tenantName).FirstOrDefaultAsync();
            return tenant;
        }

        public async Task AddTenant(Tenant tenant)
        {
            _generalDbContext.Add(tenant);
            await _generalDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _generalDbContext.Users.ToListAsync();
            return users.Count > 0 ? users : null;
        }

        public async Task<User> GetUser(int uid)
        {
            var user = await _generalDbContext.Users.Where(u => u.UserId == uid).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByUsername(string uname)
        {
            var user = await _generalDbContext.Users.Where(u => u.Username == uname).FirstOrDefaultAsync();
            return user;
        }

        public async Task AddUser(User user)
        {
            _generalDbContext.Add(user);
            await _generalDbContext.SaveChangesAsync();
        }
    }
}
