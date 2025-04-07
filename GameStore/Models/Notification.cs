namespace GameStore.Models;

public class Notification
{
    public int NotificationId { get; set; }
    
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsRead { get; set; } = false;
    
    public string NotificationImageUrl { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int OrderId { get; set; }
    
    public List<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}