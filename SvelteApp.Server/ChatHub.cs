using Microsoft.AspNetCore.SignalR;

namespace SvelteApp.Server;

public class ChatHub : Hub
{
    public async Task SendMessage(string username, string message)
    {
        Console.WriteLine($"Received message from {username}: {message}");
        await Clients.All.SendAsync("messageReceived", username, message);
    }
}
