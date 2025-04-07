using GameStore.Models;
using GameStore.Repositories;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace GameStore.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepositorie _orderRepositorie;
    private readonly UserManager<User> _userManager;
    private readonly INotificationService _notificationService;
    private readonly IUserService _userService;
    private readonly IVideogameService _videogameService;
    private readonly IVideoGameOrderRepositorie _videoGameOrderRepositorie;

    public OrderService(IOrderRepositorie orderRepositorie, UserManager<User> userManager, INotificationService notificationService, IUserService userService, IVideogameService videogameService, IVideoGameOrderRepositorie videoGameOrderRepositorie)
    {
        _orderRepositorie = orderRepositorie;
        _userManager = userManager;
        _notificationService = notificationService;
        _userService = userService;
        _videogameService = videogameService;
        _videoGameOrderRepositorie = videoGameOrderRepositorie;
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _orderRepositorie.GetOrders();
    }
    public async Task<List<Order>> GetOrdersByUserId(string id)
    {
        return await _orderRepositorie.GetOrdersByUserId(id);
    }
    public async Task AddOrder(Order order)
    {
        await _orderRepositorie.AddAsync(order);
    }

    public async Task<Order?> GetOrderByDate(DateTime date)
    {
        return await _orderRepositorie.GetOrder(o => o.OrderDate == date);
    }
    
    public async Task<Order?> GetOrderById(int orderId)
    {
        return await _orderRepositorie.GetOrder(o => o.OrderId == orderId);
    }

    public async Task GenerateOrder(List<VideoGame> videogames,User user)
    {
        var order = new Order
        {
            OrderDate = DateTime.Now,
            UserId = user.Id,
        };
        await AddOrder(order);
        var orderDb = await GetOrderByDate(order.OrderDate);
        
        var adminNotificationDb = await _notificationService.CreateAdminNotification(orderDb);
        await _userService.AddNotification(adminNotificationDb);
            
        foreach (var videoGame in videogames)
        {
            await _videoGameOrderRepositorie.AddAsync(
                new VideoGameOrder
                {
                    OrderId = orderDb.OrderId,
                    VideoGameId = videoGame.VideoGameId,
                });
            VideoGame updatedVideoGame = _videogameService.UpdateVideogameStockAndPopularity(videoGame);
            await _videogameService.UpdateVideogameDb(updatedVideoGame);
        }
    }
}