using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowclone.Models
{
    //In the Message Entity, the payload is the Content field of MessageModel converted into a byte array.
    public class Post
    {
        public Guid PostId { get; set; }

        public int MessageId { get; set; }

        public int MemberId { get; set; }

        public int ChannelId { get; set; }

        public string Content { get; set; }
    }
}
