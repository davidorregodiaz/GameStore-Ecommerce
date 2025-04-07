using Microsoft.AspNetCore.Identity;

namespace GameStore.Models;

public class User : IdentityUser
{
    public string? GamerTag { get; set; }
    public string? ProfileImageUrl { get; set; }
    
    public DateTime LastLogin { get; set; } = DateTime.Now;
    
    public List<Order>? Orders { get; set; }
    public List<Review> Reviews { get; set; } = new List<Review>();
    public List<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
    
}