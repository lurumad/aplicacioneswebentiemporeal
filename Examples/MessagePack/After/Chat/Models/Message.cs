using MessagePack;
using System;

namespace Chat.Models
{
    [MessagePackObject]
    public class Message
    {
        [Key("username")]
        public string Username { get; set; }
        [Key("text")]
        public string Text { get; set; }
        [Key("time")]
        public DateTime Time { get; set; }
    }
}
