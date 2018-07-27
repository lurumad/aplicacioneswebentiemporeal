using System;

namespace WhatsApp.Model
{
    public static class MessageExtensions
    {
        public static NewMessageDto ToDto(this Message message, Guid chatId)
        {
            return new NewMessageDto
            {
                Text = message.Text,
                ChatId = chatId,
                UserId = message.UserId,
                UserName = message.UserName,
                Time =  message.Time
            };
        }
    }
}
