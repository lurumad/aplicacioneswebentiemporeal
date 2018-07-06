using System;
using System.Collections.Generic;
using WhatsApp.Model;

namespace WhatsApp.Repositories
{
    public interface IChatRepository
    {
        IEnumerable<Chat> GetChatsBy(User user);
        User GetUserBy(int id);
        Chat GetChatBy(Guid id);
    }
}
