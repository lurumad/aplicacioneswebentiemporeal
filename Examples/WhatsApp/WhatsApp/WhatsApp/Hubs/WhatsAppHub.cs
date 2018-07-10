using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WhatsApp.Model;
using WhatsApp.Services;

namespace WhatsApp.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WhatsAppHub : Hub
    {
        private readonly IChatService chatService;

        public WhatsAppHub(IChatService chatService)
        {
            this.chatService = chatService ?? throw new System.ArgumentNullException(nameof(chatService));
        }

        public override Task OnConnectedAsync()
        {
            var chats = chatService.GetChatsBy(Context.User.Identity.Name);

            foreach (var chat in chats)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString());
            }

            return base.OnConnectedAsync();
        }

        public Task SendMessage(MessageDto message)
        {
            var user = chatService.GetUserBy(message.UserId);
            var chat = chatService.GetChatBy(message.ChatId);
            var newMessage = chat.AddMessage(user, message.Text);
            return Clients.Group(chat.Id.ToString()).SendAsync("NewMessage", newMessage.ToDto(chat.Id));
        }
    }
}
