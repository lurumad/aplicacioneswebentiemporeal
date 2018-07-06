using System.Collections.Generic;

namespace WhatsApp.Model
{
    public class WhatsAppResponse
    {
        public UserDto Me { get; set; }
        public IEnumerable<ChatDto> Chats { get; set; }
    }
}
