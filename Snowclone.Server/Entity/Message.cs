using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Snowclone.Data;

namespace Snowclone.Entities
{

    public class Message : BaseEntity
    {
        public int MessageId { get; set; }

        public int ChannelId { get; set; }

        public int MemberId { get; set; }

        public Member Member { get; set; }

        public Channel Channel { get; set; }

        //public DateTime CreatedUtc;

        public string Command { get; set; }

        public string Content { get; set; }

        //public string ChannelName { get; set; }

    }
}
