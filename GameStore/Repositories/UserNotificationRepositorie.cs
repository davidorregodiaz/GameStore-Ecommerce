using System.Linq.Expressions;
using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class UserNotificationRepositorie : BaseRepositorie<UserNotification>,IUserNotificationRepositorie
{
    public UserNotificationRepositorie(AppDbContext context) : base(context)
    {
        
    }
    
    
}