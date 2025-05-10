using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.Abstractions.Hubs;
using RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.Abstractions.Redis.RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.Abstractions.Services.RabbitMq;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.SignalR;

public sealed class ChatHub : Hub
{
    private readonly IChatHubService _chathubService;
    private readonly IRedisService _redisService;
    readonly IRabbitMQProducer _rabbitMqProducer;

    public ChatHub(IChatHubService chathubService, IRedisService redisService, IRabbitMQProducer rabbitMqProducer)
    {
        _chathubService = chathubService;
        _redisService = redisService;
        _rabbitMqProducer = rabbitMqProducer;
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
        await _rabbitMqProducer.Publish($"Id: {userId} Name: {name} Date: {DateTime.Now}");
        return userId;
    }



    public async Task SendPrivateMessage(string receiverConnectionId, string message)
    {
        await _redisService.CacheMessageAsync(Context.ConnectionId, receiverConnectionId, message, DateTime.Now);
        await _chathubService.SendPrivateMessage(receiverConnectionId, message,Context.ConnectionId);
    }
    public async Task GetChatHistory(string receiverConnectionId)
    {
        await _chathubService.GetChatHistory(Context.ConnectionId, receiverConnectionId);
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