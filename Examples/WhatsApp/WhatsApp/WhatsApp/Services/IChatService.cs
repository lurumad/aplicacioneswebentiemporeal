using System;
using WhatsApp.Model;

namespace WhatsApp.Services
{
    public interface IChatService
    {
        WhatsAppResponse GetMe();
        User GetUserBy(int id);
        Chat GetChatBy(Guid id);
    }
}
