using GameStore.Models;
using GameStore.Repositories;

namespace GameStore.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepositorie _notificationRepositorie;

    public NotificationService(INotificationRepositorie notificationRepositorie)
    {
        _notificationRepositorie = notificationRepositorie;
    }

    public async Task<Notification?> CreateAdminNotification(Order orderDb)
    {
        Notification adminNotification = new Notification
        {
            Message = "Se ha generado una nueva order",
            Role = "Admin",
            OrderId = orderDb.OrderId
        };
        await  _notificationRepositorie.AddAsync(adminNotification);
        
        return await _notificationRepositorie.GetAsync(n => n.Date == adminNotification.Date);
    }
}