using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.Abstractions.Hubs;
using RealTimeChatApp.SignalR.HubServices;

namespace RealTimeChatApp.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddTransient<IChatHubService,ChatHubService>();
            services.AddSignalR()
             .AddStackExchangeRedis(RedisConfiguration.RedisHost);
        }
    }
}