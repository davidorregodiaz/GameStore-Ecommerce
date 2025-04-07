using GameStore.Models;
using GameStore.Repositories;
using GameStore.Util;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserNotificationRepositorie _userNotificationRepositorie;

    public UserService(UserManager<User> userManager, IUserNotificationRepositorie userNotificationRepositorie)
    {
        _userManager = userManager;
        _userNotificationRepositorie = userNotificationRepositorie;
    }

    public async Task AddNotification(Notification notification)
    {
        var adminUsers = _userManager.GetUsersInRoleAsync(Roles.Admin).GetAwaiter().GetResult();

        foreach (var adminUser in adminUsers)
        {
            UserNotification userNotification = new UserNotification
            {
                UserId = adminUser.Id,
                NotificationId = notification.NotificationId
            };
            await _userNotificationRepositorie.AddAsync(userNotification);
        }
    }
}