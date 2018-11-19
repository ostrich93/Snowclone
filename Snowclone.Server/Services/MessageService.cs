using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snowclone.Entities;

namespace Snowclone.Services
{
    public class MessageService
    {
        private TenantDbContext _context;

        public MessageService(TenantDbContext context)
        {
            _context = context;
        }

        public async Task Add(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMemberMessages(string handle)
        {
            var messages = await _context.Messages.Where(m => m.Member.Handle == handle).ToListAsync();
            return messages.Count > 0 ? messages : null;
        }

        public async Task<List<Message>> GetMemberMessages(int mid)
        {
            var messages = await _context.Messages.Where(m => m.MemberId == mid).ToListAsync();
            return messages.Count > 0 ? messages : null;
        }

        public async Task<List<Message>> GetChannelMessages(string channelName)
        {
            var messages = await _context.Messages.Where(m => m.Channel.ChannelName == channelName).ToListAsync();
            return messages.Count > 0 ? messages : null;
        }

        public async Task EditMessage(Message message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }

        public async Task EditMessage(Message message, string editedContent)
        {
            message.Content = editedContent;
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}
