using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class NotificationRepositorie : BaseRepositorie<Notification>, INotificationRepositorie
{
    private readonly AppDbContext _context;
    private readonly DbSet<Notification> _notifications;
    
    public NotificationRepositorie(AppDbContext context) : base(context)
    {
        _context = context;
        _notifications = _context.Set<Notification>();
    }

    public async Task UpdateNotification(Notification notification)
    {
        if (notification != null)
        {
            _context.Update(notification);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Notification>> GetNotificationsAsync()
    {
        return await _notifications
            .Include(n => n.UserNotifications)
            .ThenInclude(un => un.User)
            .ToListAsync();
    }

    public async Task<List<Notification>> GetUserNotifications(string? userId = null)
    {
        return await _notifications
            .Include(n => n.UserNotifications)
            .ThenInclude(n => n.Notification)
            .ToListAsync();
    }
    
}