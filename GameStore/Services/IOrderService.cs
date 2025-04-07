using GameStore.Models;

namespace GameStore.Services;

public interface IOrderService
{
    Task AddOrder(Order order);
    Task<Order?> GetOrderByDate(DateTime date);
    Task<Order?> GetOrderById(int orderId);
    Task<IEnumerable<Order>> GetOrders();
    Task<List<Order>> GetOrdersByUserId(string id);
    Task GenerateOrder(List<VideoGame> videoGames,User user);
}