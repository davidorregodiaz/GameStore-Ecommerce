using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class VideoGameOrderRepositorie : BaseRepositorie<VideoGameOrder>, IVideoGameOrderRepositorie
{
    private readonly AppDbContext _context;
    private readonly DbSet<VideoGameOrder> _dbSet;
    
    public VideoGameOrderRepositorie(AppDbContext context) : base(context)
    {
        _context = context;
        _dbSet = _context.Set<VideoGameOrder>();
    }
}