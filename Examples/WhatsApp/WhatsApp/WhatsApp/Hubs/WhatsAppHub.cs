using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WhatsApp.Model;
using WhatsApp.Services;

namespace WhatsApp.Hubs
{
    public class WhatsAppHub : Hub
    {
        private readonly IChatService chatService;

        public WhatsAppHub(IChatService chatService)
        {
            this.chatService = chatService ?? throw new System.ArgumentNullException(nameof(chatService));
        }

        public Task SendMessage(MessageDto message)
        {
            var user = chatService.GetUserBy(message.UserId);
            var chat = chatService.GetChatBy(message.ChatId);
            var newMessage = chat.AddMessage(user, message.Text);
            if (chat.IsGroup)
            {
                return Clients.Group(chat.Id.ToString()).SendAsync("NewMessage", newMessage.ToDto(chat.Id));
            }
            else
            {
                return Clients.Group(chat.Id.ToString()).SendAsync("NewMessage", newMessage.ToDto(chat.Id));
            }
        }
    }
}
