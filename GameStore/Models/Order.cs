using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace GameStore.Models;

public class Order
{
    public int OrderId { get; set; }

    public Boolean Accepted { get; set; } = false;
    
    public DateTime OrderDate { get; set; }
    
    
    public List<VideoGame> videoGames { get; set; } = new List<VideoGame>();
    
    
    public List<VideoGameOrder> videoGameOrders { get; set; } = new List<VideoGameOrder>();
    
    public string UserId { get; set; }
    public User? User { get; set; }
}