using GameStore.Models;

namespace GameStore.Services;

public interface INotificationService
{
    Task<Notification?> CreateAdminNotification(Order orderDb);
}