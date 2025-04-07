using System.Linq.Expressions;
using GameStore.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class BaseRepositorie<T> : IRepositorie<T> where T : class
{
    
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepositorie(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public async Task<List<T>> GetAllAsync(string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop);
            }

            return await query.ToListAsync();
        }
        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveAsync();
    }

    public async Task DeleteAsync(int id)
    { 
        var entity = await _dbSet.FindAsync(id);
        _dbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate,string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }
        return await query.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}