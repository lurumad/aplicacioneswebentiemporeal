using System;
using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private static int users = 0;

        public Task SendMessage(Message message)
        {
            return Clients.All.SendAsync("message", message);
        }

        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync("connected", ++users);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return Clients.All.SendAsync("disconnected", --users);
        }
    }
}
