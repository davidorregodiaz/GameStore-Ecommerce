namespace GameStore.Models;

public class UserNotification
{
    public int UserNotificationId { get; set; }
    
    public string UserId { get; set; }
    public User? User { get; set; }
    
    public int NotificationId { get; set; }
    public Notification? Notification { get; set; }
}