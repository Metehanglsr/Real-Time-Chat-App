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

        public Task NotifyTyping(string receiverConnectionId)
        {
            return _hubContext.Clients.Client(receiverConnectionId)
                .SendAsync("ReceiveTypingNotification");
        }
        public async Task SendPrivateMessage(string receiverConnectionId, string message)
        {
            var receiverClient = ClientSource.Clients.FirstOrDefault(c => c.ConnectionId == receiverConnectionId);

            if (receiverClient != null)
            {
                await _hubContext.Clients.Client(receiverClient.ConnectionId).SendAsync("ReceiveMessage", message);
            }
        }
    }
}
