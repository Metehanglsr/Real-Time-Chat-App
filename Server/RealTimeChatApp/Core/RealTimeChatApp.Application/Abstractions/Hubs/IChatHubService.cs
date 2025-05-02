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
        Task SendPrivateMessage(string receiverConntectionId, string message);
        //Task SendGroupMessage(ChatMessageDto message);
        Task JoinGroup(string userId, string groupName);
        Task LeaveGroup(string userId, string groupName);
        Task NotifyTyping(string receiverUserId);
    }

}
