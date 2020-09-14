using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Data.Interfaces;
using EzGame.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Contorllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;
        public ProductsController(IUnitOfWork db, IToastNotification notification )
        {
            _db = db;
            _notification = notification;
        }

        //Get 
        [HttpGet]
        [Route("/Products/Index")]
        public async Task<IActionResult> Index(string name,int pageid = 1,int sortby=1)
        {
            ViewBag.PageID = pageid;

            switch (sortby)
            {
                case 1:
                    // همه
                    var games = await _db.GameRepository.GetAllAsync();
                    ViewBag.CountPage = games.Count(a => !a.IsDeleted);
                    var Games = _db.GameRepository.Paging(25, pageid, games);
                    if (Games == null || ViewBag.CountPage == 0)
                    {
                        ViewBag.GamesNull = "هیچ پستی موجود نیست";
                        _notification.AddWarningToastMessage("پستی موجود نیست");
                        return View();
                    }
                    return View(Games);
                case 2:
                    //جدید ترین ها
                    var GamesByLatestProduc = _db.GameRepository.GetAll().Where(a => !a.IsDeleted).OrderByDescending(p => p.CreatedTime);
                    ViewBag.CountPage = GamesByLatestProduc.Count(a => !a.IsDeleted);
                    if (GamesByLatestProduc == null || ViewBag.CountPage == 0)
                    {
                        ViewBag.GamesNull = "هیچ پستی موجود نیست";
                        _notification.AddWarningToastMessage("پستی موجود نیست");
                        return View();
                    }
                    var getPageByLatestProduc = _db.GameRepository.Paging(25, pageid, GamesByLatestProduc);
                    return View(getPageByLatestProduc);
                case 3:
                    // ارزان ترین
                    var GamesByCheapestProducts = _db.GameRepository.GetAll().Where(a => !a.IsDeleted).OrderBy(p => p.GameEditions[0].Price);
                    ViewBag.CountPage = GamesByCheapestProducts.Count(a => !a.IsDeleted);
                    if (GamesByCheapestProducts == null || ViewBag.CountPage == 0)
                    {
                        ViewBag.GamesNull = "هیچ پستی موجود نیست";
                        _notification.AddWarningToastMessage("پستی موجود نیست");
                        return View();
                    }
                    var getPageByCheapestProducts = _db.GameRepository.Paging(25, pageid, GamesByCheapestProducts);
                    return View(getPageByCheapestProducts);
                case 4:
                    // گران ترین
                    var GamesByMostExpensiveProducts = _db.GameRepository.GetAll().Where(a => !a.IsDeleted).OrderByDescending(p => p.GameEditions[0].Price);
                    ViewBag.CountPage = GamesByMostExpensiveProducts.Count(a => !a.IsDeleted);
                    if (GamesByMostExpensiveProducts == null|| ViewBag.CountPage==0)
                    {
                        ViewBag.GamesNull = "هیچ پستی موجود نیست";
                        _notification.AddWarningToastMessage("پستی موجود نیست");
                        return View();
                    }
                    var getPageByMostExpensiveProducts = _db.GameRepository.Paging(25, pageid, GamesByMostExpensiveProducts);
                    return View(getPageByMostExpensiveProducts);
            }
            return View(null);

        }


        // مشخصات
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notification.AddErrorToastMessage("بازی پیدا نشد !");
                return RedirectToAction(nameof(Index));
            }
            var Game = (await _db.GameRepository.GetByIdAsync(id));
            if (Game == null)
            {
                _notification.AddErrorToastMessage("بازی پیدا نشد !");
                return RedirectToAction(nameof(Index));
            }
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            foreach (var item in Game.GameVisit)
            {
                if (item.Ip == ip)
                {
                    return View(Game);
                }
            }
            var gameVisit = new GameVisit()
            {
                Ip = ip,
                GameId = Game.Id,
                CreatedTime = DateTime.Now,
            };
            _db.GameVisitRepository.Insert(gameVisit);
            Game.Visit +=1;
            _db.GameRepository.Update(Game);
            _db.SaveChange();
            return View(Game);
        }

    }
}
