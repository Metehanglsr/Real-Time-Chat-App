using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChatApp.Application.DTOs;

namespace RealTimeChatApp.Application.Abstractions.Redis
{
    namespace RealTimeChatApp.Application.Abstractions.Redis
    {
        public interface IRedisService
        {
            Task SetUserOnlineAsync(string userId);
            Task RemoveUserAsync(string userId);
            Task SetUserNameAsync(string userId, string name);
            Task<string?> GetUserNameAsync(string userId);
            Task<List<UserDto>> GetOnlineUsersAsync();
            Task<UserDto?> GetUserByConnectionIdAsync(string connectionId);
        }
    }
}
