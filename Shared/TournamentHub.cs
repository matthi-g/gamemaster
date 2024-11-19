using Microsoft.AspNetCore.SignalR;

namespace Shared;

public class TournamentHub : Hub
{
    public async Task BroadcastMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}