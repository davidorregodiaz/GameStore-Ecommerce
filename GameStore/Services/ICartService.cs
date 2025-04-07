using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Services;

public interface ICartService
{
    string DeleteFromCart(int videogameId,List<VideoGame> videogames);
    Task<string> AddToCart(int videogameId,string sessionCart);
}