using System;
using System.Collections.Generic;
using System.Linq;
using WhatsApp.Model;

namespace WhatsApp.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly User Vincent = new User { Id = 1, Name = "Vincent", Avatar = "/images/chat_avatar_01.jpg" };
        private readonly User Aiden = new User { Id = 2, Name = "Aiden", Avatar = "/images/chat_avatar_02.jpg" };
        private readonly User Mike = new User { Id = 3, Name = "Mike", Avatar = "/images/chat_avatar_03.jpg" };
        private readonly User Erica = new User { Id = 4, Name = "Erica", Avatar = "/images/chat_avatar_04.jpg" };
        private readonly User Ginger = new User { Id = 5, Name = "Ginger", Avatar = "/images/chat_avatar_05.jpg" };
        private readonly User Tracy = new User { Id = 6, Name = "Tracy", Avatar = "/images/chat_avatar_06.jpg" };
        private readonly User Christian = new User { Id = 7, Name = "Christian", Avatar = "/images/chat_avatar_07.jpg" };
        private readonly User Monica = new User { Id = 8, Name = "Monica", Avatar = "/images/chat_avatar_08.jpg" };
        private readonly User Dean = new User { Id = 9, Name = "Dean", Avatar = "/images/chat_avatar_09.jpg" };
        private readonly User Peyton = new User { Id = 10, Name = "Peyton", Avatar = "/images/chat_avatar_10.jpg" };
        private IEnumerable<User> users;
        private IEnumerable<Chat> chats;

        public ChatRepository()
        {
            users = new[] { Vincent, Aiden, Mike, Erica, Ginger, Tracy, Christian, Monica, Dean, Peyton };
            chats = new[]
            {
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Vincent, Aiden } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Vincent, Mike } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Vincent, Erica } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Vincent, Ginger } },
                new Chat { Name = "Party", IsGroup = true, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Vincent, Aiden, Erica } },
                new Chat { Name = "Tennis", IsGroup = true, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Vincent, Aiden, Mike } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Aiden, Monica } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Aiden, Ginger } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Aiden, Christian } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Aiden, Peyton } },
                new Chat { Name = "TV Series", IsGroup = true, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Peyton, Aiden, Christian } },
                new Chat { Name = "Friends", IsGroup = true, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Monica, Aiden, Ginger, Mike } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Mike, Erica } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Mike, Ginger } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Ginger, Peyton } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Ginger, Tracy } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Monica, Ginger } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Monica, Peyton } },
                new Chat { IsGroup = false, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Monica, Tracy } },
                new Chat { Name = "Beers", IsGroup = true, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Peyton, Aiden, Christian, Monica, Aiden, Ginger, Mike } },
                new Chat { Name = "Dinners", IsGroup = true, Unread = 0, Messages = new List<Message>(), Participants = new List<User>() { Vincent, Peyton, Dean } },
            };
        }

        public IEnumerable<Chat> GetAllChats()
        {
            return chats;
        }

        public Chat GetChatBy(Guid id)
        {
            return chats.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Chat> GetChatsBy(User user)
        {
            return chats.Where(c => c.Participants.Any(p => p.Id == user.Id));
        }

        public IEnumerable<Chat> GetChatsBy(string username)
        {
            return chats.Where(c => c.Participants.Any(p => p.Name.Equals(username, StringComparison.InvariantCultureIgnoreCase)));
        }

        public User GetUserBy(int id)
        {
            return users.SingleOrDefault(u => u.Id == id);
        }

        public User GetUserBy(string name)
        {
            return users.SingleOrDefault(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
