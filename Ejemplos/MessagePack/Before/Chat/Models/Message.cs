using MessagePack;
using System;

namespace Chat.Models
{
    public class Message
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
