using System;
using System.Collections.Generic;

//not a model, an actual entity
public class Message
{
    public int MessageId { get; set; }

    public string Content { get; set; }

    public int ChannelId { get; set; }

    public string ChannelName { get; set; }

    public int PosterId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public Dictionary<string, string> Headers;

    public string ContentType; //might be enum instead?

    public long ContentLength { get { return Payload.Length; } }

    public byte[] Payload;

	public Message()
	{

	}
}
