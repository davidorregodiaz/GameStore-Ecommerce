using System.Linq.Expressions;
using GameStore.Models;

namespace GameStore.Repositories;

public interface IOrderRepositorie : IRepositorie<Order>
{
    Task<IEnumerable<Order>> GetOrders();
    Task<Order?> GetOrder(Expression<Func<Order, bool>> predicate);
    Task<Order?> GetOrderById(int id);
    Task<List<Order>> GetOrdersByUserId(string userId);
    Task UpdateAsync(Order order);
}