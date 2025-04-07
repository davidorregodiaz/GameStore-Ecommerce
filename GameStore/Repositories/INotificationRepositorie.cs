using GameStore.Models;

namespace GameStore.Repositories;

public interface INotificationRepositorie :IRepositorie<Notification>
{
     Task<List<Notification>> GetNotificationsAsync();

     Task<List<Notification>> GetUserNotifications(string? userId = null);
     Task UpdateNotification(Notification notification);

}