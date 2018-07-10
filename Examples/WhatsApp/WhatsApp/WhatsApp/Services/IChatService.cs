using System;
using System.Collections.Generic;
using WhatsApp.Model;

namespace WhatsApp.Services
{
    public interface IChatService
    {
        WhatsAppResponse GetMe(string name);
        User GetUserBy(int id);
        User GetUserBy(string name);
        Chat GetChatBy(Guid id);
        IEnumerable<Chat> GetAllChats();
        IEnumerable<Chat> GetChatsBy(string username);
    }
}
