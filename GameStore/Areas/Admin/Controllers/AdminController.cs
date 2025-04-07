using System.Text.Json;
using GameStore.Models;
using GameStore.Repositories;
using GameStore.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using FileStream = System.IO.FileStream;

namespace GameStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        
        private readonly IVideoGameRepositorie _videoGameRepository;
        private readonly IGenreRepositorie _genreRepository;
        private readonly IOrderRepositorie _orderRepository;
        private readonly INotificationRepositorie _notificationRepository;
        private readonly IUserNotificationRepositorie _userNotificationRepository;
        IWebHostEnvironment _webHostEnvironment;

        public AdminController(IVideoGameRepositorie videoGameRepository, IGenreRepositorie genreRepository, IWebHostEnvironment webHostEnvironment, IOrderRepositorie orderRepository, INotificationRepositorie notificationRepository, IUserNotificationRepositorie userNotificationRepository)
        {
            _videoGameRepository = videoGameRepository;
            _genreRepository = genreRepository;
            _webHostEnvironment = webHostEnvironment;
            _orderRepository = orderRepository;
            _notificationRepository = notificationRepository;
            _userNotificationRepository = userNotificationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Upsert(int? id)
        {
            
            VideoGameVm videoGameVm = new VideoGameVm
            {
                VideoGame = new VideoGame(),
                Genres =  _genreRepository.GetAllAsync().Result.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.GenreId.ToString()
                }).ToList()
            };
            
            if (id == null || id == 0)
            {
                
                return View(videoGameVm);    
            }
            
            var videoGameToEdit = await _videoGameRepository.GetAsync(p => p.VideoGameId == id,includeProperties: "Reviews,Genre");
            videoGameVm.VideoGame = videoGameToEdit;

            
            return View(videoGameVm);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(VideoGameVm videoGameVm,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string pathName = Path.Combine(wwwRootPath, "images");

                    if (!string.IsNullOrEmpty(videoGameVm.VideoGame.ImageUrl))
                    {
                        var oldPath = videoGameVm.VideoGame.ImageUrl;
                        
                        if (System.IO.File.Exists(Path.Combine(wwwRootPath, oldPath)))
                        {
                            System.IO.File.Delete(Path.Combine(wwwRootPath,oldPath));
                        }
                    }
                    
                    using (var fileStream = new FileStream(Path.Combine(pathName, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    videoGameVm.VideoGame.ImageUrl = @"\images\"+fileName;
                }
                
                if (videoGameVm.VideoGame.VideoGameId == 0 || videoGameVm.VideoGame.VideoGameId == null)
                {
                    
                    await _videoGameRepository.AddAsync(videoGameVm.VideoGame);
                    await _videoGameRepository.SaveAsync();
                    TempData["Success"] = "Video game added successfully";
                    TempData["ImageUrl"] = videoGameVm.VideoGame.ImageUrl;
                    TempData["GameName"] = videoGameVm.VideoGame.Name;
                    return RedirectToAction("Index");
                }    
                await _videoGameRepository.UpdateAsync(videoGameVm.VideoGame);
                await _videoGameRepository.SaveAsync();
                TempData["Success"] = "Video game updated successfully";
                TempData["ImageUrl"] = videoGameVm.VideoGame.ImageUrl;
                TempData["GameName"] = videoGameVm.VideoGame.Name;
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var videogames = await _videoGameRepository.GetAllAsync(includeProperties:"Genre,Reviews");
            
            return Json(new
            {
                data = videogames
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            VideoGame? deletedVideoGame = await _videoGameRepository.GetAsync(u=>u.VideoGameId == id);

            if (!string.IsNullOrEmpty(deletedVideoGame.ImageUrl))
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileUrl = deletedVideoGame.ImageUrl.TrimStart('\\');
                string oldPath = Path.Combine(wwwRootPath, fileUrl);

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }
            
            await _videoGameRepository.DeleteAsync(id);
            await _videoGameRepository.SaveAsync();
            return Json(new
            {
                success = true
            });
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult OrdersRecord()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetOrders();
            
            return Json(
                new
                {
                    data = orders
                });
        }
        
        public async Task<IActionResult> OrderInfo(int id)
        {
            var order = await _orderRepository.GetAsync(u=>u.OrderId==id);
            
            return View(order);
        }

        public async Task<IActionResult> OrderInfoJson(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            
            return Json(new
            {
                data = order
            });
        }

        [HttpPost]
        public async Task<IActionResult> AcceptOrder(int id)
        {
            var order = _orderRepository.GetAsync(o=>o.OrderId==id).GetAwaiter().GetResult();
            order.Accepted = true;
            await _orderRepository.UpdateAsync(order);

            Notification customerNotification = new Notification
            {
                Date = DateTime.Now,
                Message = "Order accepted",
                OrderId = order.OrderId
            };
            
            await _notificationRepository.AddAsync(customerNotification);
            await _notificationRepository.SaveAsync();
            
            var customerNotificationDb = await _notificationRepository.GetAsync(cn => cn.Date == customerNotification.Date);
            
            UserNotification notification = new UserNotification
            {
                NotificationId = customerNotificationDb.NotificationId,
                UserId = order.UserId
            };
            await _userNotificationRepository.AddAsync(notification);
            await _userNotificationRepository.SaveAsync();
            
        return Ok();
        }
    }
}
