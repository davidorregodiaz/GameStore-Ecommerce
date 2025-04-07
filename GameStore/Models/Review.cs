using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace GameStore.Models;

public class Review
{
    public int ReviewId { get; set; }
    
    public int Rating { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Comment { get; set; } = string.Empty;
    
    [ValidateNever]
    public DateTime DateCreated { get; set; }
    
    public int? VideoGameId { get; set; }
    [Newtonsoft.Json.JsonIgnore] 
    public VideoGame? VideoGame { get; set; }
    
    public string? UserId { get; set; }
    
    [Newtonsoft.Json.JsonIgnore] 
    public User? User { get; set; }
    
}