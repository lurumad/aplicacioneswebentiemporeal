using System;

namespace WhatsApp.Model
{
    public class MessageDto
    {
        public Guid ChatId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
