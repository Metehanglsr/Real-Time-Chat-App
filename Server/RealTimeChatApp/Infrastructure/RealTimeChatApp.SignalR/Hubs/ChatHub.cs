using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.Abstractions.Hubs;
using RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.Abstractions.Redis.RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.SignalR;

public sealed class ChatHub : Hub
{
    private readonly IChatHubService _chathubService;
    private readonly IRedisService _redisService;

    public ChatHub(IChatHubService chathubService, IRedisService redisService)
    {
        _chathubService = chathubService;
        _redisService = redisService;
    }

    public async Task<string> SetUsername(string name)
    {
        var userId = Context.ConnectionId;

        var isAlreadyOnline = await _redisService.IsUserOnlineAsync(userId);
        if (!isAlreadyOnline)
        {
            await _redisService.SetUserOnlineAsync(userId);
            await _redisService.SetUserNameAsync(userId, name);
            await Clients.All.SendAsync(ReceiveFunctionNames.UserOnline, userId);
        }

        var users = await _redisService.GetOnlineUsersAsync();
        await Clients.All.SendAsync(ReceiveFunctionNames.ReceiveUsers, users);

        return userId;
    }



    public async Task SendPrivateMessage(string receiverConnectionId, string message)
    {
        await _chathubService.SendPrivateMessage(receiverConnectionId, message);
    }

    public async Task NotifyTyping(string receiverConnectionId)
    {
        await _chathubService.NotifyTyping(receiverConnectionId);
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.ConnectionId;
        await _redisService.RemoveUserAsync(userId);
        await Clients.All.SendAsync(ReceiveFunctionNames.UserOffline, userId);
        await base.OnDisconnectedAsync(exception);
    }
}