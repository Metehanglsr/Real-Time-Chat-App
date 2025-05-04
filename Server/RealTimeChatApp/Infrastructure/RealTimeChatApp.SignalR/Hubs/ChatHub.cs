using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.Abstractions.Hubs;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.SignalR.Data;

public sealed class ChatHub : Hub
{

    private readonly IChatHubService _chathubService;

    public ChatHub(IChatHubService chathubService)
    {
        _chathubService = chathubService;
    }

    public string GetName(string name)
    {
        var exists = ClientSource.Clients.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
        if (exists == null)
        {
            ChatClient chatClient = new ChatClient()
            {
                ConnectionId = Context.ConnectionId,
                Name = name
            };
            ClientSource.Clients.Add(chatClient);
        }
        Clients.All.SendAsync("ReceiveUsers", ClientSource.Clients);
        return Context.ConnectionId;
    }
    public async Task SendPrivateMessage(string receiverConnectionId, string message)
    {
        await _chathubService.SendPrivateMessage(receiverConnectionId, message);
    }
    public async Task NotifyTyping(string receiverConnectionId)
    {
        await _chathubService.NotifyTyping(receiverConnectionId);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var client = ClientSource.Clients.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
        if (client != null)
        {
            ClientSource.Clients.Remove(client);
            Clients.All.SendAsync("ReceiveUsers", ClientSource.Clients);
        }
        return base.OnDisconnectedAsync(exception);
    }
}