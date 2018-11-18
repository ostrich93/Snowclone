using System;
using System.Collections.Generic;
using Snowclone.Data;

namespace Snowclone.Entities
{
    //Note: NOT the same as a model.
    public class Channel
    {

        public int ChannelId { get; set; }

        public string ChannelName { get; set; }

        public bool isPrivate { get; set; }

        public List<Message> Messages { get; set; }

        public List<Member> BanList { get; set; } //might replace with IPBanList instead?

        public List<string> IPBans { get; set; }

        //public int TenantId { get; set; } //Tenant is a server

        public Channel()
        {

        }
    }
}
