using System;

namespace WhatsApp.Model
{
    public class Message
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
