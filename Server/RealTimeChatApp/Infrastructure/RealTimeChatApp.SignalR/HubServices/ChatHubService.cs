using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.Abstractions.Hubs;
using RealTimeChatApp.Application.Abstractions.Redis.RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.DTOs;

namespace RealTimeChatApp.SignalR.HubServices
{
    public sealed class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IRedisService _redisService;

        public ChatHubService(IHubContext<ChatHub> hubContext, IRedisService redisService)
        {
            _hubContext = hubContext;
            _redisService = redisService;
        }

        public Task NotifyTyping(string receiverConnectionId)
        {
            return _hubContext.Clients.Client(receiverConnectionId)
                .SendAsync(ReceiveFunctionNames.ReceiveTypingNotification);
        }

        public async Task SendPrivateMessage(string receiverConnectionId, string message, string senderId)
        {
            var receiverClient = await _redisService.GetUserByConnectionIdAsync(receiverConnectionId);
            var senderClient = await _redisService.GetUserByConnectionIdAsync(senderId);
            if (receiverClient != null)
            {
                await _hubContext.Clients.Client(receiverClient.ConnectionId).SendAsync(ReceiveFunctionNames.ReceiveMessage, new
                {
                    SenderName = senderClient?.Name,
                    Message = message
                });
            }
        }
        public async Task GetChatHistory(string senderId,string receiverId)
        {
            var cachedMessages = await _redisService.GetCachedMessagesAsync(senderId,receiverId);
            await _hubContext.Clients.Client(senderId).SendAsync(ReceiveFunctionNames.ReceiveChatHistory, cachedMessages);
        }
    }
}
