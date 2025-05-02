using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.SignalR.Data;

public sealed class ChatHub : Hub
{
    public void GetName(string name)
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