using System;
using System.Collections.Generic;
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
            var skip = (pageid - 1) * 25;
            var count = await _db.GameRepository.CountAsync();
            if(count == 0)
            {
                ViewBag.GamesNull = "هیچ پستی موجود نیست";
                _notification.AddWarningToastMessage("پستی موجود نیست");
                return View();
            }
            ViewBag.PageID = pageid;
            ViewBag.CountPage = count;
            var Games = (await _db.GameRepository.GetPage(skip));
            return View(Games);
        }

        //[HttpGet]
        //[Route("/Products/TheMostVisited")]
        //public IActionResult TheMostVisited()
        //{
        //    var Games =_db.GameRepository.Where(p=>p.);
        //    return View("Index",Games);
        //}

        [HttpGet]
        [Route("/Products/TheLatestProduct")]
        public IActionResult TheLatestProduct()
        {
            var Games = _db.GameRepository.GetAll().Where(a => !a.IsDeleted).OrderByDescending(p => p.CreatedTime);
            if (Games == null)
            {
                ViewBag.GamesNull = "هیچ پستی موجود نیست";
                _notification.AddWarningToastMessage("پستی موجود نیست");
                return View();
            }
            return View("Index", Games);
        }

        [HttpGet]
        [Route("/Products/TheCheapestProducts")]
        public IActionResult TheCheapestProducts()
        {
            var Games = _db.GameRepository.GetAll().Where(a=>!a.IsDeleted).OrderBy(p=>p.GameEditions[0].Price);
            if (Games == null)
            {
                ViewBag.GamesNull = "هیچ پستی موجود نیست";
                _notification.AddWarningToastMessage("پستی موجود نیست");
                return View();
            }
            return View("Index", Games);
        }
        
        [HttpGet]
        [Route("/Products/TheMostExpensiveProducts")]
        public IActionResult TheMostExpensiveProducts()
        {
            var Games = _db.GameRepository.GetAll().Where(a => !a.IsDeleted).OrderByDescending(p => p.GameEditions[0].Price);
            if (Games == null)
            {
                ViewBag.GamesNull = "هیچ پستی موجود نیست";
                _notification.AddWarningToastMessage("پستی موجود نیست");
                return View();
            }
            return View("Index", Games);
        }
    }
}
