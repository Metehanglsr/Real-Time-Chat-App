using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChatApp.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace RealTimeChatApp.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication app)
        {
            app.MapHubs<ChatHub>("/chathub");
        }
    }
}
