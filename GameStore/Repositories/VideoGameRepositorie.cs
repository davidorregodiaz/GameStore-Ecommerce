using System.Linq.Expressions;
using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class VideoGameRepositorie : BaseRepositorie<VideoGame>, IVideoGameRepositorie
{
    private readonly AppDbContext _context;
    private readonly DbSet<VideoGame> _videoGames;
    
    public VideoGameRepositorie(AppDbContext context) : base(context)
    {
        _context = context;
        _videoGames = _context.Set<VideoGame>();
    }

    public async Task UpdateAsync(VideoGame? videoGame)
    {
        if (videoGame != null)
        {
            _videoGames.Update(videoGame);
            await SaveAsync();
        }
    }

    public Task<VideoGame?> GetVideoGameWithAll(Expression<Func<VideoGame, bool>> predicate)
    {
        var videoGame = _videoGames.
                                            Where(predicate)
                                            .Include(p => p.Genre)
                                            .Include(a=> a.Reviews)
                                            .ThenInclude(p=>p.User);
        return Task.FromResult(videoGame.FirstOrDefault());
    }
}