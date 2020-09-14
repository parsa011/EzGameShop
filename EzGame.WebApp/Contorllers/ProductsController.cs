using System.Linq;
using System.Threading.Tasks;
using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Contorllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;

        public ProductsController(IUnitOfWork db, IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }

        //Get 
        [HttpGet]
        public async Task<IActionResult> Index(int pageid = 1)
        {
            var games = await _db.GameRepository.GetAllAsync();
            ViewBag.CountPage = games.Count(a=>!a.IsDeleted);
            ViewBag.PageID = pageid;
            if (ViewBag.CountPage == 0)
            {
                ViewBag.GamesNull = "هیچ پستی موجود نیست";
                _notification.AddWarningToastMessage("پستی موجود نیست");
                return View();
            }
            var Games = _db.GameRepository.Paging(25, pageid, games);
            return View(Games);
        }
    }
}
