using System.Linq.Expressions;
using GameStore.Models;

namespace GameStore.Repositories;

public interface IVideoGameRepositorie : IRepositorie<VideoGame>
{
    Task UpdateAsync(VideoGame videoGame);
    Task<VideoGame?> GetVideoGameWithAll(Expression<Func<VideoGame, bool>> predicate);
}