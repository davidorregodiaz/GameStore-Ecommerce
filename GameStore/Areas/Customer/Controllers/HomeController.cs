using GameStore.Data;
using GameStore.Models;
using GameStore.Repositories;
using GameStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameStore.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly INotificationRepositorie _notificationRepositorie;
    
    //SERVICES
    private readonly IVideogameService _videogameService;
    private readonly IReviewService _reviewService;
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;

    public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, INotificationRepositorie notificationRepositorie, IVideogameService videogameService, IReviewService reviewService, IOrderService orderService, ICartService cartService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _notificationRepositorie = notificationRepositorie;
        _videogameService = videogameService;
        _reviewService = reviewService;
        _orderService = orderService;
        _cartService = cartService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any,NoStore = false)]
    public async Task<IActionResult> AllGames()
    {
        IEnumerable<VideoGame> videoGames = await _videogameService.GetAllVideogames();
        return View(videoGames.ToList());
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var videoGameDetails = await _videogameService.GetVideoGameWithRelations(id);
        
        GameReviewVm gameReviewVm = new GameReviewVm
        {
            videoGame = videoGameDetails,
            review = new Review()
        };
        
        return View(gameReviewVm);
    }
    
    [HttpPost] 
    public async Task<IActionResult> Details(GameReviewVm videoGameReviewVm)
    {
        if (ModelState.IsValid)
        {
            videoGameReviewVm.review.DateCreated = DateTime.Now;
            await _reviewService.AddReview(videoGameReviewVm.review);
        }
        var newGameReviewVm = await _videogameService.CreateGameReviewVm(videoGameReviewVm);
        
        return View(newGameReviewVm);
    }

    public IActionResult ShoppingCart()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CartId")))
        {
            List<VideoGame> videoGames = JsonConvert.DeserializeObject<List<VideoGame>>(HttpContext.Session.GetString("CartId"));
            return View(videoGames);
        }
        return View();
        
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int id)
    {
        var sessionCart = HttpContext.Session.GetString("CartId");
        var newList = await _cartService.AddToCart(id, sessionCart);
        
        if (!string.IsNullOrEmpty(newList))
        {
            HttpContext.Session.SetString("CartId", newList);
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    public IActionResult DeleteFromCart(int id)
    {
        var videogames = JsonConvert.DeserializeObject<List<VideoGame>>(HttpContext.Session.GetString("CartId"));
        var newVideogames = _cartService.DeleteFromCart(id,videogames);
        HttpContext.Session.SetString("CartId", newVideogames);
        return RedirectToAction("ShoppingCart");
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        if (_signInManager.IsSignedIn(User))
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            
            List<VideoGame> videogames = JsonConvert.DeserializeObject<List<VideoGame>>(HttpContext.Session.GetString("CartId"));
            HttpContext.Session.SetString("CartId", "");
            
            if (videogames != null)
            {
                await _orderService.GenerateOrder(videogames,user);
                return Json(new { success = true });    
            }
            
        }
        return Json(new { success = false });
    }

    public async Task<IActionResult> OrderInfo(int id)
    {
        var order = await _orderService.GetOrderById(id);
        return View(order);
    }
    
    public async Task<IActionResult> OrderInfoJson(int id)
    {
        return Json(new
        {
            data = await _orderService.GetOrderById(id)
        });
    }

    [HttpPost]
    public async Task<IActionResult> ReadOrder(int id)
    {
        await _notificationRepositorie.DeleteAsync(id);
        return Ok();
    }

    public IActionResult OrdersRecord()
    {
        return View();
    }
    
    public async Task<IActionResult> OrdersRecordJson()
    {
        var user = await _userManager.GetUserAsync(User);
        var ordersByUserId = await _orderService.GetOrdersByUserId(user.Id);
        
        return Json(new
        {
            data = ordersByUserId
        });
    }

    public async Task<IActionResult> Popular()
    {
        var videoGames = await _videogameService.GetAllVideogames();
        return View(videoGames);
    }
    
}