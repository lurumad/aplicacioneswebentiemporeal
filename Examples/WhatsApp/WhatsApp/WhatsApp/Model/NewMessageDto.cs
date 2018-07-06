using System;

namespace WhatsApp.Model
{
    public class NewMessageDto
    {
        public Guid ChatId { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
