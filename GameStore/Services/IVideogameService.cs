using GameStore.Models;

namespace GameStore.Services;

public interface IVideogameService
{
    public Task<IEnumerable<VideoGame>> GetAllVideogames();
    public Task<VideoGame> GetVideoGameWithRelations(int? id);
    
    public Task<GameReviewVm> CreateGameReviewVm(GameReviewVm? gameReviewVm);
    
    public VideoGame UpdateVideogameStockAndPopularity(VideoGame videoGame);
    
    public Task UpdateVideogameDb(VideoGame videoGame);
    
}