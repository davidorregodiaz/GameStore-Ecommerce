using GameStore.Models;
using Microsoft.AspNetCore.SignalR;

namespace GameStore.Hubs;

public class NotificationHub : Hub 
{
    public async Task SendNotification(string userId,int orderId)
    {
        Notification notification = new Notification
        {
            Message = "Order accepted",
            OrderId = orderId
        };
        
        Console.WriteLine(notification.ToString());
        Console.WriteLine("Hello world");
        
        await Clients.User(userId).SendAsync("ReceiveNotification", notification);
    }
}