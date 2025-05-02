using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.Abstractions.Hubs;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.SignalR.Data;

namespace RealTimeChatApp.SignalR.HubServices
{
    public sealed class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task JoinGroup(string userId, string groupName)
        {
            throw new NotImplementedException();
        }

        public Task LeaveGroup(string userId, string groupName)
        {
            throw new NotImplementedException();
        }

        public Task NotifyTyping(string receiverUserId)
        {
            return _hubContext.Clients.User(receiverUserId).SendAsync("ReceiveTypingNotification");
        }



        public async Task SendPrivateMessage(string receiverConntectionId, string message)
        {
            var receiverClient = ClientSource.Clients.FirstOrDefault(c => c.ConnectionId == receiverConntectionId);

            if (receiverClient != null)
            {
                await _hubContext.Clients.Client(receiverClient.ConnectionId).SendAsync("ReceiveMessage", message);
            }
        }
    }
}
