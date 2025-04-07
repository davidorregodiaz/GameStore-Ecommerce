using GameStore.Models;
using GameStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameStore.Services;

public class CartService : ICartService
{
    
    private readonly IVideoGameRepositorie _videoGameRepository;

    public CartService(IVideoGameRepositorie videoGameRepository)
    {
        _videoGameRepository = videoGameRepository;
    }

    public string DeleteFromCart(int videogameId,List<VideoGame> videogames)
    {
        if (videogames != null)
        {
            videogames.Remove(videogames.FirstOrDefault(p => p.VideoGameId == videogameId));
            string serialized = JsonConvert.SerializeObject(videogames);
            return serialized;
        }
        return "";
    }

    public async Task<string> AddToCart(int videogameId,string sessionCart)
    {
        List<VideoGame> videogames;
        if (string.IsNullOrEmpty(sessionCart))
        {
            videogames = new List<VideoGame>();
        }
        else
        {
            videogames = JsonConvert.DeserializeObject<List<VideoGame>>(sessionCart);
        }
        var videogame =  await _videoGameRepository.GetVideoGameWithAll(p => p.VideoGameId == videogameId);
        
        foreach (var videoGame in videogames)
        {
            if (videogame.VideoGameId == videoGame.VideoGameId)
            {
                return "";
            }
        }
        videogames.Add(videogame);
        string serialized = JsonConvert.SerializeObject(videogames);
        return serialized;
    }
}