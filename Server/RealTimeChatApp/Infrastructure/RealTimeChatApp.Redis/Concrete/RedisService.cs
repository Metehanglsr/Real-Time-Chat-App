using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.Abstractions.Redis.RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.DTOs;
using StackExchange.Redis;

namespace RealTimeChatApp.Redis.Concrete
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _db;

        public RedisService()
        {
            var redis = ConnectionMultiplexer.Connect(RedisConfiguration.RedisHost);
            _db = redis.GetDatabase();
        }

        public async Task SetUserOnlineAsync(string userId)
        {
            await _db.StringSetAsync($"online:{userId}", true, TimeSpan.FromMinutes(30));
        }

        public async Task RemoveUserAsync(string userId)
        {
            await _db.KeyDeleteAsync($"online:{userId}");
        }

        public async Task SetUserNameAsync(string userId, string name)
        {
            await _db.StringSetAsync($"username:{userId}", name);
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var name = await _db.StringGetAsync($"username:{userId}");
            return name;
        }

        public async Task<List<UserDto>> GetOnlineUsersAsync()
        {
            var server = ConnectionMultiplexer.Connect("localhost:6379").GetServer("localhost", 6379);
            var keys = server.Keys(pattern: "username:*").Select(k => k.ToString().Replace("username:", "")).ToList();

            var users = new List<UserDto>();
            foreach (var key in keys)
            {
                var name = await _db.StringGetAsync($"username:{key}");
                if(name != RedisValue.Null)
                {
                    users.Add(new UserDto { ConnectionId = key, Name = name! });
                }
            }

            return users;
        }
        public async Task<UserDto?> GetUserByConnectionIdAsync(string connectionId)
        {
            var name = await _db.StringGetAsync($"username:{connectionId}");
            if (name == RedisValue.Null)
                return null;

            return new UserDto
            {
                ConnectionId = connectionId,
                Name = name!
            };
        }

        public async Task<bool> IsUserOnlineAsync(string connectionId)
        {
            return await _db.KeyExistsAsync($"user:{connectionId}");
        }
    }

}
