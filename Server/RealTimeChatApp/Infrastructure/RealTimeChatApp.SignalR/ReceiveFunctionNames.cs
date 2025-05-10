using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChatApp.SignalR
{
    public static class ReceiveFunctionNames
    {
        public const string ReceiveMessage = "ReceiveMessage";
        public const string UserOffline = "UserOffline"; 
        public const string UserOnline = "UserOnline";
        public const string ReceiveUsers = "ReceiveUsers";
        public const string ReceiveTypingNotification = "ReceiveTypingNotification"; 
        public const string ReceiveChatHistory = "ReceiveChatHistory";
    }
}