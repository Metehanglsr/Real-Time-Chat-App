using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.Abstractions.Redis.RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Redis.Concrete;

namespace RealTimeChatApp.Redis
{
    public static class ServiceRegistration
    {
        public static void AddRedisServices(this IServiceCollection services)
        {
            services.AddSingleton<IRedisService, RedisService>();
        }
    }
}
