using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowclone.Entities;
using Microsoft.EntityFrameworkCore;

namespace Snowclone.Services
{
    public class MemberService
    {
        private TenantDbContext _context;

        public MemberService(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User uInfo)
        {
            var member = new Member { UserId = uInfo.UserId, Handle = uInfo.Username, Email = uInfo.Email, Password = uInfo.Password, IsBanned = false };
            _context.Add(member);
            await _context.SaveChangesAsync();
            return member.MemberId;
        }

        public async Task<int> Add(Member member)
        {
            _context.Add(member);
            await _context.SaveChangesAsync();
            return member.MemberId;
        }

        public async Task<Member> FindByUser(int uid)
        {
            var member = await _context.Members.FirstOrDefaultAsync(u => u.UserId == uid);
            return member;
        }

        public async Task<Member> FindByUsername(string uname)
        {
            var member = await _context.Members.Where(u => u.Handle == uname).FirstOrDefaultAsync();
            return member;
        }

        public async Task<List<Member>> GetMembers()
        {
            var members = await _context.Members.ToListAsync();
            return members.Count > 0 ? members : null;
        }
    }
}
