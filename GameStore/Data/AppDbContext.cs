using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityDbContext = Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext;

namespace GameStore.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    DbSet<VideoGame> VideoGames { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Notification> Notifications { get; set; }
    DbSet<UserNotification> UserNotifications { get; set; }
    DbSet<VideoGameOrder> VideoGameOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Review>()
            .HasOne(u => u.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UserNotification>()
            .HasOne(un => un.User)
            .WithMany(u => u.UserNotifications)
            .HasForeignKey(un => un.UserId);
        
        modelBuilder.Entity<UserNotification>()
            .HasOne(un => un.Notification)
            .WithMany(u => u.UserNotifications)
            .HasForeignKey(un => un.NotificationId);
        
        modelBuilder.Entity<VideoGameOrder>()
            .HasOne(vgo => vgo.VideoGame)
            .WithMany(vg => vg.VideoGameOrders)
            .HasForeignKey(vgo => vgo.VideoGameId);
        
        modelBuilder.Entity<VideoGameOrder>()
            .HasOne(vgo => vgo.Order)
            .WithMany(o => o.videoGameOrders)
            .HasForeignKey(vgo => vgo.OrderId);
        
        // modelBuilder.Entity<VideoGameShoppingCart>()
        //     .HasKey(vgsc => new { vgsc.VideoGameId, vgsc.ShoppingCartId });
        //
        // modelBuilder.Entity<VideoGameShoppingCart>()
        //     .HasOne(vgsc => vgsc.VideoGame)
        //     .WithMany(vg => vg.VideoGameShoppingCarts)
        //     .HasForeignKey(vgsc => vgsc.VideoGameId);
        //
        // modelBuilder.Entity<VideoGameShoppingCart>()
        //     .HasOne(vgsc => vgsc.ShoppingCart)
        //     .WithMany(sc => sc.VideoGameShoppingCarts)
        //     .HasForeignKey(vgsc => vgsc.ShoppingCartId);
        
        // modelBuilder.Entity<User>()
        //     .HasOne(p=>p.ShoppingCart)
        //     .WithOne(p=>p.User)
        //     .HasForeignKey<ShoppingCart>(p=>p.UserId);
        
        modelBuilder.Entity<VideoGame>()
            .HasMany(e => e.Reviews)
            .WithOne(e => e.VideoGame)
            .HasForeignKey(e => e.VideoGameId)
            .IsRequired();
        
        
        modelBuilder.Entity<Genre>().HasData(
            new Genre
            {
                GenreId = 1,
                Name = "Stealth"
            },
            new Genre
            {
                GenreId = 2,
                Name = "RPG"
            },
            new Genre
            {
                GenreId = 3,
                Name = "Strategy"
            },
            new Genre
            {
                GenreId = 4,
                Name = "Role"
            });
        
        // modelBuilder.Entity<Review>().HasData(
        //     new Review
        //     {
        //         ReviewId = 1,
        //         Comment = "I love this video game",
        //         Rating = 9.5M,
        //         VideoGameId = 1,
        //         UserId = 1
        //     },
        //     new Review
        //     {
        //         ReviewId = 2,
        //         Comment = "I hate this video game,no recommendation",
        //         Rating = 1M,
        //         VideoGameId = 2,
        //         UserId = 2
        //     },
        //     new Review
        //     {
        //         ReviewId = 3,
        //         Comment = "I Like this video game,is so good",
        //         Rating = 10M,
        //         VideoGameId = 2,
        //         UserId = 1
        //     });
        
        // modelBuilder.Entity<User>().HasData(
        //     new User
        //     {
        //         UserId = 1,
        //         Email = "david@gmail.com",
        //         FirstName = "David",
        //         LastName = "Orrego",
        //         Password = "david"
        //     },
        //     new User
        //     {
        //         UserId = 2,
        //         Email = "daniel@gmail.com",
        //         FirstName = "Daniel",
        //         LastName = "Orrego",
        //         Password = "daniel"
        //     },
        //     new User
        //     {
        //         UserId = 3,
        //         Email = "MK@gmail.com",
        //         FirstName = "MK",
        //         LastName = "Vidal",
        //         Password = "mk"
        //     });
    }
}