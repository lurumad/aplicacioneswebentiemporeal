using System;
using System.Collections.Generic;

namespace WhatsApp.Model
{
    public class Chat
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Unread { get; set; }
        public bool IsGroup { get; set; }
        public List<User> Participants { get; set; }
        public List<Message> Messages { get; set; }
        public string Image { get; set; }

        public Message AddMessage(User user, string text)
        {
            var message = new Message { UserId = user.Id, UserName = user.Name, Text = text, Time = DateTime.UtcNow };
            Messages.Add(message);
            return message;
        }
    }
}
