using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChatApp.Application.DTOs;

namespace RealTimeChatApp.SignalR.Data
{
    public static class ClientSource
    {
        public static List<ChatClient> Clients = new List<ChatClient>();
    }
}
