using System.Linq.Expressions;
using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class OrderRepositorie : BaseRepositorie<Order>, IOrderRepositorie
{
    private readonly AppDbContext _context;
    private readonly DbSet<Order> _orders;
    
    public OrderRepositorie(AppDbContext context) : base(context)
    {
        _context = context;
        _orders = context.Set<Order>();
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _orders
            .Include(u=>u.User)
            .Include(vgo => vgo.videoGameOrders)
            .ThenInclude(vg => vg.VideoGame)
            .ToListAsync();
    }
    
    public async Task<List<Order>> GetOrdersByUserId(string userId)
    {
        return await _orders
            .Where(o => o.UserId == userId)
            .Include(u => u.User)
            .Include(vgo => vgo.videoGameOrders)
            .ThenInclude(vg => vg.VideoGame)
            .ToListAsync();
    }

    public async Task<Order?> GetOrder(Expression<Func<Order, bool>> predicate)
    {
        return await _orders
            .Include(vgo => vgo.videoGameOrders)
            .ThenInclude(vg => vg.VideoGame)
            .FirstOrDefaultAsync(predicate);
            
    }
    
    public async Task UpdateAsync(Order? order)
    {
        if (order != null)
        {
            _orders.Update(order);
            await SaveAsync();
        }
    }
    
    public async Task<Order?> GetOrderById(int id)
    {
        return await _orders
            .Include(vgo => vgo.videoGameOrders)
            .ThenInclude(vg => vg.VideoGame)
            .FirstOrDefaultAsync(o => o.OrderId == id);
            
    }
    
}