using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChatApp.Application.DTOs;

namespace RealTimeChatApp.Application.Abstractions.Hubs
{
    public interface IChatHubService
    {
        Task SendPrivateMessage(string receiverConnectionId, string message, string senderId);
        Task NotifyTyping(string receiverUserId);
        Task GetChatHistory(string senderId, string receiverId);

    }

}
