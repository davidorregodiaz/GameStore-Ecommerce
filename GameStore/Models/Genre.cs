using System.ComponentModel.DataAnnotations;

namespace GameStore.Models;

public class Genre
{
    public int GenreId { get; set; }
    [Required]
    [StringLength(40)]
    public string Name { get; set; } = string.Empty;
}