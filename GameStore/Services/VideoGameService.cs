using GameStore.Models;
using GameStore.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace GameStore.Services;

public class VideoGameService : IVideogameService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IVideoGameRepositorie _videoGameRepository;

    public VideoGameService(IVideoGameRepositorie videoGameRepository, IMemoryCache memoryCache)
    {
        _videoGameRepository = videoGameRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<VideoGame>> GetAllVideogames()
    {
        if (!_memoryCache.TryGetValue("Videogames", out List<VideoGame>? videogames))
        {
            videogames = await _videoGameRepository.GetAllAsync("Reviews,Genre");

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set("Videogames", videogames, cacheOptions);
        }
        
        return videogames;
    }

    public async Task<VideoGame> GetVideoGameWithRelations(int? id)
    {
        return await _videoGameRepository.GetVideoGameWithAll(p => p.VideoGameId == id);
    }

    public async Task<GameReviewVm> CreateGameReviewVm(GameReviewVm? gameReviewVm)
    {
        return new GameReviewVm
        {
            videoGame = await GetVideoGameWithRelations(gameReviewVm.review.VideoGameId),
            review = new Review()
        };
    }

    public VideoGame UpdateVideogameStockAndPopularity(VideoGame videoGame)
    {
        return new VideoGame
        {
            VideoGameId = videoGame.VideoGameId,
            Desarrollador = videoGame.Desarrollador,
            Price = videoGame.Price,
            ImageUrl = videoGame.ImageUrl,
            Description = videoGame.Description,
            Name = videoGame.Name,
            Stock = videoGame.Stock-1,
            Popularity = videoGame.Popularity+1,
            ReleaseDate = videoGame.ReleaseDate,
            GenreId = videoGame.GenreId
        };
    }

    public async Task UpdateVideogameDb(VideoGame videoGame)
    {
        await _videoGameRepository.UpdateAsync(videoGame);
    }
}