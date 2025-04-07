using Newtonsoft.Json;

namespace GameStore.Models;

public class VideoGameOrder
{
    [JsonIgnore]
    public int VideoGameOrderId { get; set; }
    public int OrderId { get; set; }
    
    
    public Order Order { get; set; }
    
    public int VideoGameId { get; set; }
    public VideoGame VideoGame { get; set; }
}