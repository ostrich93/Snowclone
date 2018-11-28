using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snowclone.Entities;

namespace Snowclone.Services
{
    public class TenantServices
    {
        private readonly string _connectionString;

        public TenantServices(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Channel>> GetChannels(int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var channelList = await context.Channels.ToListAsync();
                return channelList.Count > 0 ? channelList : null;
            }
        }

        public async Task<Channel> GetChannel(int channelId, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var channel = await context.Channels.FindAsync(channelId);
                return channel;
            }
        }

        public async Task AddChannel(Channel channel, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                context.Add(channel);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateChannel(Channel channel, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                context.Update(channel);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteChannel(Channel channel, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                context.Remove(channel);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Member>> GetMembers(int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var memberList = await context.Members.ToListAsync();
                return memberList.Count > 0 ? memberList : null;
            }
        }

        public async Task<Member> GetMember(int memberId, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var member = await context.Members.Where(m => m.MemberId == memberId).FirstOrDefaultAsync();
                return member;
            }
        }

        public async Task AddMember(Member member, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                context.Add(member);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateMember(Member member, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                context.Update(member);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Member> FindMemberByName(string memberName, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var member = await context.Members.Where(m => m.Username == memberName).FirstOrDefaultAsync();
                return member;
            }
        }

        public async Task<Member> FindMemberByHandle(string handle, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var member = await context.Members.Where(m => m.Handle == handle).FirstOrDefaultAsync();
                return member;
            }
        }

        public async Task<List<Message>> GetAllMessages(int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var messageList = await context.Messages.ToListAsync();
                return messageList.Count > 0 ? messageList : null;
            }
        }

        public async Task<List<Message>> GetChannelMessages(int channelId, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var channelMessages = await context.Messages.Where(msg => msg.ChannelId == channelId).ToListAsync();
                return channelMessages.Count > 0 ? channelMessages : null;
            }
        }

        public async Task<List<Message>> GetMemberMessages(int memberId, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                var memberMessages = await context.Messages.Where(msg => msg.MemberId == memberId).ToListAsync();
                return memberMessages.Count > 0 ? memberMessages : null;
            }
        }

        public async Task AddMessage(Message message, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                context.Add(message);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateMessage(Message message, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                context.Update(message);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteMessage(Message message, int tenantId)
        {
            using (var context = CreateDbContext(tenantId))
            {
                context.Remove(message);
                await context.SaveChangesAsync();
            }
        }

        private TenantDbContext CreateDbContext(int tenantId)
        {
            return new TenantDbContext(); //add in variables once the constructor is defined with sharding
        }
    }
}
