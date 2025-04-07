using GameStore.Models;

namespace GameStore.Services;

public interface IUserService
{
    Task AddNotification(Notification notification);
}