using System;
using System.Collections.Generic;
using System.Text;
using Snowclone.Models;
using System.Runtime.Serialization;

namespace Snowclone.Entities
{

    public enum MessageType { Request, Response };

    //not a model, an actual entity
    [Serializable]
    public class Message
    {
        public string ChannelId;

        public string MessageId;

        public string SenderId;

        public string RecipientId; //can be a Server's id or a User's

        public string MemberId;

        public DateTime CreatedUtc;

        public DateTime? ExpirationUtc;

        public bool? Success;

        public string Command;

        public string Email;

        public string Password;

        public string ChannelName;

        public string ContentType;

        //public strig ContentEncoding;

        public long? ContentLength;

        public Dictionary<string, string> CustomHeaders = new Dictionary<string, string>();

        public byte[] Data;

        public Message()
        {

        }

        public Message(byte[] data)
        {
            //all of the fields of the Message are derived from the payload.
            if (data == null || data.Length < 1) throw new ArgumentException(nameof(data));

            string headerString = Encoding.UTF8.GetString(data);
            string[] lines = headerString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            if (lines == null || lines.Length < 1) throw new ArgumentException("Cannot get the headers from supplied data");

            int lineCount = lines.Length;

            for (int a = 0; a < lineCount; a++)
            {
                string currentLine = lines[a];
                if (String.IsNullOrEmpty(currentLine)) break;

                int headerLength;
                string key = "";
                string value = "";

                if (currentLine.Contains(":"))
                {
                    headerLength = currentLine.Length;
                    string[] keyvalPair = currentLine.Split(':');
                    key = keyvalPair[0].Trim();
                    value = keyvalPair[1].Trim();
                }
                else
                {
                    key = currentLine.Trim();
                    value = null;
                }

                switch (key.ToLower())
                {
                    case "success":
                        Success = !String.IsNullOrEmpty(value) ? Convert.ToBoolean(value) : false;
                        break;
                    case "email":
                        Email = value;
                        break;
                    case "password":
                        Password = value;
                        break;
                    case "channel":
                        ChannelId = value;
                        break;
                    case "sender":
                        SenderId = value;
                        break;
                    case "server":
                        RecipientId = value;
                        break;
                    case "recipient":
                        RecipientId = value;
                        break;
                    case "messageid": //used for edit or delete message 
                        MessageId = value;
                        break;
                    case "target": //used for PMs or bans/unbans, etc.
                        RecipientId = value;
                        break;
                    case "createdat":
                        CreatedUtc = DateTime.Parse(value);
                        break;
                    case "expiration":
                        ExpirationUtc = DateTime.Parse(value);
                        break;
                    case "channelname":
                        ChannelName = value;
                        break;
                    case "command":
                        Command = value;
                        break;
                    case "contentlength":
                        try
                        {
                            ContentLength = Convert.ToInt64(value);
                        }
                        catch (Exception)
                        {
                            throw new ArgumentException("Content Length must be in a form convertable to long data type.");
                        }
                        break;
                    default:
                        if (!CustomHeaders.ContainsKey(key))
                            CustomHeaders.Add(key, value);
                        break;
                }

                if (ContentLength != null && ContentLength > 0 && ContentLength < data.Length)
                {
                    Data = new byte[Convert.ToInt32(ContentLength)];
                    Buffer.BlockCopy(data, (data.Length - Convert.ToInt32(ContentLength)), Data, 0, Convert.ToInt32(ContentLength));
                }
            }
        }

        public override string ToString()
        {
            string stringForm = "";
            stringForm += "MessageId: " + MessageId + Environment.NewLine;
            stringForm += "SenderId: " + SenderId + Environment.NewLine;
            if (!String.IsNullOrEmpty(RecipientId)) stringForm += "RecipientId: " + RecipientId + Environment.NewLine;
            stringForm += "CreatedAt: " + CreatedUtc.ToString("MM/dd/yyyy HH:mm:ss") + Environment.NewLine;
            stringForm += "Expiration: " + (ExpirationUtc != null ? (Convert.ToDateTime(ExpirationUtc).ToString("MM/dd/yyyy HH:mm:ss")) : "null") + Environment.NewLine;
            stringForm += "Command: " + (!String.IsNullOrEmpty(Command) ? Command : "null") + Environment.NewLine;

            if (!String.IsNullOrEmpty(Command)) {
                if (Success != null) stringForm += "Success: " + Success.ToString() + Environment.NewLine;
                else stringForm += "Success: false" + Environment.NewLine;
            }
            if (!String.IsNullOrEmpty(Email)) stringForm += "Email: " + Email + Environment.NewLine;
            if (!String.IsNullOrEmpty(Password)) stringForm += "Password: " + Password + Environment.NewLine;
            if (!String.IsNullOrEmpty(MemberId)) stringForm += "MemberId: " + MemberId + Environment.NewLine;
            if (!String.IsNullOrEmpty(ChannelId)) stringForm += "ChannelId: " + ChannelId + Environment.NewLine;
            if (!String.IsNullOrEmpty(ChannelName)) stringForm += "ChannelName: " + ChannelName + Environment.NewLine;

            stringForm += "ContentType: " + (!String.IsNullOrEmpty(ContentType) ? ContentType : "null") + Environment.NewLine;
            stringForm += "ContentLength: " + Convert.ToString(ContentLength) + Environment.NewLine;

            if (CustomHeaders != null && CustomHeaders.Count > 0)
            {
                foreach (string key in CustomHeaders.Keys)
                {
                    stringForm += key + ": " + CustomHeaders[key] + Environment.NewLine;
                }
            }

            if (Data != null)
            {
                string dString = Encoding.UTF8.GetString(Data);
                stringForm += ContentLength != null ? String.Format("Data ({0} bytes)", ContentLength) : "Data: (Content Length unspecified)";
                stringForm += Environment.NewLine + "\t" + dString + Environment.NewLine;
            }
            else
            {
                stringForm += "Data (null)" + Environment.NewLine;
            }

            return stringForm;
        }

        //create a byte array from the entire MessageObject that can be transmitted to a stream
        public byte[] ToBytes()
        {
            StringBuilder header = new StringBuilder();
            string headerString = "";
            byte[] headerBytes;
            byte[] messageBytes;

            if (SanitizeString(MessageId, out string sanitizedMid)) header.Append("MessageId: " + sanitizedMid + Environment.NewLine);
            if (SanitizeString(SenderId, out string sanitizedSid)) header.Append("Sender: " + sanitizedSid + Environment.NewLine);
            if (SanitizeString(RecipientId, out string sanitizedRid)) header.Append("Recipient: " + sanitizedRid + Environment.NewLine);
            if (CreatedUtc != null) header.Append("CreatedAt: " + CreatedUtc.ToUniversalTime().ToString("MM/dd/yyyy HH:mm:ss.fffffff") + Environment.NewLine);
            if (ExpirationUtc != null) header.Append("Expiration: " + Convert.ToDateTime(ExpirationUtc).ToUniversalTime().ToString("MM/dd/yyyy HH:mm:ss.fffffff") + Environment.NewLine);
            if (SanitizeString(Command, out string sanitizedcmd)) header.Append("Command: " + sanitizedcmd + Environment.NewLine);
            if (Success != null) header.Append("Success: " + Success + Environment.NewLine);
            if (SanitizeString(Email, out string sanitizedmail)) header.Append("Email: " + sanitizedmail + Environment.NewLine);
            if (SanitizeString(Password, out string sanitizedpass)) header.Append("Password: " + sanitizedpass + Environment.NewLine);
            if (SanitizeString(MemberId, out string sanitizedUid)) header.Append("Member: " + sanitizedUid + Environment.NewLine);
            if (SanitizeString(ChannelId, out string sanitizedCid)) header.Append("Channel: " + sanitizedCid + Environment.NewLine);
            if (SanitizeString(ChannelName, out string sanitizedCnm)) header.Append("ChannelName: " + sanitizedCnm + Environment.NewLine);
            if (SanitizeString(ContentType, out string sanitizedctyp)) header.Append("ContentType: " + sanitizedctyp + Environment.NewLine);
            
            if (Data != null && Data.Length > 0) header.Append("ContentLength: " + Data.Length + Environment.NewLine);

            if (CustomHeaders != null && CustomHeaders.Count > 0)
            {
                foreach (KeyValuePair<string, string> curr in CustomHeaders)
                {
                    if (String.IsNullOrEmpty(curr.Key)) continue;

                    string sanitizedKey = "";
                    if (SanitizeString(curr.Key, out sanitizedKey))
                    {
                        string sanitizedValue = "";
                        if (SanitizeString(curr.Value, out sanitizedValue))
                        {
                            header.Append(sanitizedKey + ": " + sanitizedValue + Environment.NewLine);
                        }
                    }
                }
            }

            header.Append(Environment.NewLine);
            headerString = header.ToString();
            headerBytes = Encoding.UTF8.GetBytes(headerString);

            if (Data == null)
            {
                messageBytes = new byte[headerBytes.Length];
                Buffer.BlockCopy(headerBytes, 0, messageBytes, 0, headerBytes.Length);
            }
            else
            {
                messageBytes = new byte[headerBytes.Length + Data.Length];
                Buffer.BlockCopy(headerBytes, 0, messageBytes, 0, headerBytes.Length);
                Buffer.BlockCopy(Data, 0, messageBytes, headerBytes.Length, Data.Length);
            }

            return messageBytes;
        }

        public bool IsValid()
        {
            List<string> errorMessages = new List<string>();
            if (String.IsNullOrEmpty(MessageId)) errorMessages.Add("MessageId is missing!");
            if (String.IsNullOrEmpty(SenderId)) errorMessages.Add("SenderId is missing!");
            if (CreatedUtc == null) errorMessages.Add("CreatedUtc is empty!");
            if (Data != null)
            {
                if (ContentLength == null)
                {
                    errorMessages.Add("ContentLength is missing!");
                }
                if (ContentLength != Data.Length)
                {
                    errorMessages.Add("ContentLength does not match length of data.");
                }
            }

            if (errorMessages.Count > 0)
            {
                Console.WriteLine("Message is invalid. These are the following errors: ");
                foreach (string error in errorMessages) Console.WriteLine(error);
            }

            return errorMessages.Count == 0;
        }

        public Message Redact()
        {
            Email = null;
            Password = null;
            return this;
        }

        public bool SanitizeString(string dirtystring, out string sanitized)
        {
            sanitized = null;
            if (String.IsNullOrEmpty(dirtystring)) return false;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(dirtystring);
            for (int i = 0; i < dirtystring.Length; i++)
            {
                byte abyte = asciiBytes[i];
                if (abyte == 0 || abyte < 32 || abyte > 126) continue;
                else sanitized += dirtystring[i];
            }
            return true;
        }
    }
}
