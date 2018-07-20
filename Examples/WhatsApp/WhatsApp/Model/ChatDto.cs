using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsApp.Model
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Unread { get; set; }
        public bool IsGroup { get; set; }
        public IEnumerable<User> Participants { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public string Image { get; set; }
    }
}
