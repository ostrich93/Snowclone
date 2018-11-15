﻿using System;
using System.Collections.Generic;

//Note: NOT the same as a model.
public class Channel
{

    public int ChannelId { get; set; }

    public string ChannelName { get; set; }

    public bool isPrivate { get; set; }

    public List<Member> Members { get; set; }

    public List<Message> Messages { get; set; }

    public List<Member> BanList { get; set; } //might replace with IPBanList instead?

    public int TenantId { get; set; }

	public Channel()
	{

	}
}
