
using GameStore.Data;
using GameStore.Hubs;
using GameStore.Models;
using GameStore.Repositories;
using GameStore.Services;
using GameStore.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();

builder.Services.AddIdentity<User,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<IVideoGameRepositorie, VideoGameRepositorie>();
builder.Services.AddScoped<IGenreRepositorie, GenreRepositorie>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IReviewRepositorie, ReviewRepositorie>();
builder.Services.AddScoped<IOrderRepositorie, OrderRepositorie>();
builder.Services.AddScoped<IVideoGameOrderRepositorie, VideoGameOrderRepositorie>();
builder.Services.AddScoped<INotificationRepositorie, NotificationRepositorie>();
builder.Services.AddScoped<IUserNotificationRepositorie,UserNotificationRepositorie>();

builder.Services.AddHostedService<CleanupUserService>();

//SERVICES
builder.Services.AddScoped<IVideogameService, VideoGameService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseResponseCaching();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapHub<NotificationHub>("/notifications");
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();