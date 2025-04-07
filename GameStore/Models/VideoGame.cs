using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace GameStore.Models;

public class VideoGame
{
    [Key]
    [ValidateNever]
    public int VideoGameId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [ValidateNever]
    public DateTime ReleaseDate { get; set; }
    
    [Required]
    public int Stock { get; set; }
    
    [ValidateNever]
    public string ImageUrl { get; set; } = string.Empty;

    [ValidateNever] public int Popularity { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Desarrollador { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
    
    [JsonIgnore]
    public List<VideoGameOrder> VideoGameOrders { get; set; } = new List<VideoGameOrder>();
    public List<Review> Reviews { get; set; } = new List<Review>();
}