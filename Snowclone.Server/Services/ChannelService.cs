using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snowclone.Entities;

namespace Snowclone.Services
{
    public class ChannelService
    {
        private TenantDbContext _context;

        public ChannelService(TenantDbContext context)
        {
            _context = context;
        }

        public async Task Add(Channel channel)
        {
            _context.Add(channel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Channel>> GetAllChannels()
        {
            var channels = await _context.Channels.ToListAsync();
            return channels;
        }

        public async Task Update(Channel channel)
        {
            _context.Update(channel);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Channel channel)
        {
            _context.Remove(channel);
            await _context.SaveChangesAsync();
        }
    }
}
