using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.SignalR.Hubs;

namespace RealTimeChatApp.SignalR.HubServices
{
    public sealed class ChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendMessageToAllAsync(string user,string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ReceiveMessage,user,message);
        }
    }
}
