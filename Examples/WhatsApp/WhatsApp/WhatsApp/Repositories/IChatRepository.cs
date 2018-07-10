using System;
using System.Collections.Generic;
using WhatsApp.Model;

namespace WhatsApp.Repositories
{
    public interface IChatRepository
    {
        IEnumerable<Chat> GetChatsBy(User user);
        User GetUserBy(int id);
        User GetUserBy(string name);
        Chat GetChatBy(Guid id);
        IEnumerable<Chat> GetAllChats();
        IEnumerable<Chat> GetChatsBy(string username);
    }
}
