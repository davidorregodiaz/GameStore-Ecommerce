using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.Models;

public class VideoGameVm
{
    public VideoGame? VideoGame { get; set; }
    public List<SelectListItem>? Genres { get; set; }
}