using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;

        public GenreController(IUnitOfWork db, IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }
        
        // GET
        public IActionResult Index()
        {
            _notification.AddInfoToastMessage("با موفقیت اضافه شده است");
            return View();
        }
    }
}