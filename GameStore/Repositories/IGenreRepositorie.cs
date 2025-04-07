using GameStore.Models;

namespace GameStore.Repositories;

public interface IGenreRepositorie : IRepositorie<Genre>
{
    Task Update(Genre genre);
}