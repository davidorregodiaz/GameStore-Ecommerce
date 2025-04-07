using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class GenreRepositorie : BaseRepositorie<Genre>, IGenreRepositorie
{
    private readonly AppDbContext _context;
    private readonly DbSet<Genre> _genres;
    
    public GenreRepositorie(AppDbContext context) : base(context)
    {
        _context = context;
        _genres = _context.Set<Genre>();
    }

    public async Task Update(Genre? genre)
    {
        if (genre != null)
        {
            _genres.Update(genre);
            await SaveAsync();
        }else throw new System.ArgumentNullException(nameof(genre));
        
    }
}