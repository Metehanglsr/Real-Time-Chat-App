using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.Abstractions.Hubs;
using RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.Abstractions.Redis.RealTimeChatApp.Application.Abstractions.Redis;
using RealTimeChatApp.Application.DTOs;

public sealed class ChatHub : Hub
{
    private readonly IChatHubService _chathubService;
    private readonly IRedisService _redisService;

    public ChatHub(IChatHubService chathubService, IRedisService redisService)
    {
        _chathubService = chathubService;
        _redisService = redisService;
    }

    // Kullanıcı bağlandığında Redis’e kaydediyoruz
    public override async Task OnConnectedAsync()
    {
        var userId = Context.ConnectionId;
        await _redisService.SetUserOnlineAsync(userId);
        await Clients.All.SendAsync("UserOnline", userId);
        await base.OnConnectedAsync();
    }

    // Kullanıcı adı belirleme
    public async Task SetUsername(string name)
    {
        var userId = Context.ConnectionId;

        // Redis üzerinde kullanıcıyı kaydetme
        await _redisService.SetUserNameAsync(userId, name);
        var user = await _redisService.GetUserNameAsync(userId);

        // Tüm kullanıcılara güncellenmiş kullanıcı listesi gönder
        var users = await _redisService.GetOnlineUsersAsync();
        await Clients.All.SendAsync("ReceiveUsers", users);

        // Kullanıcıyı bildirme
        await Clients.Caller.SendAsync("ReceiveUsers", users);

        await Task.FromResult(userId);
    }

    public async Task SendPrivateMessage(string receiverConnectionId, string message)
    {
        await _chathubService.SendPrivateMessage(receiverConnectionId, message);
    }

    public async Task NotifyTyping(string receiverConnectionId)
    {
        await _chathubService.NotifyTyping(receiverConnectionId);
    }

    // Kullanıcı çıkarken Redis’ten siliyoruz
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.ConnectionId;
        await _redisService.RemoveUserAsync(userId);
        await Clients.All.SendAsync("UserOffline", userId);
        await base.OnDisconnectedAsync(exception);
    }
}
