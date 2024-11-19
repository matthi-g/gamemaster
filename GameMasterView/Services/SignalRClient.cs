using Microsoft.AspNetCore.SignalR.Client;

namespace GameMasterView.Services;

public class SignalRClient
{
    private HubConnection _connection;
    
    public event Action<string, string>? OnMessageReceived;
    
    public async Task ConnectAsync()
    {
        try
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5000/tournamentHub")
                .Build();
        
            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                OnMessageReceived?.Invoke(user, message);
                Console.WriteLine($"Received message: {user}: {message}");
            });
        
            await _connection.StartAsync();
            Console.WriteLine("SignalR connected!");
        } 
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to SignalR hub: {ex.Message}");
        }
    }
    
    public async Task SendMessage(string user, string message)
    {
        await _connection.InvokeAsync("BroadcastMessage", user, message);
    }
    
}