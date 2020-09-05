using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Common.ViewModel.Home;
using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Contorllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;

        public HomeController(IUnitOfWork db, IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }

        //Get 
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel()
            {
                Games = (await _db.GameRepository.GetAllAsync(a => !a.IsDeleted)),
                platforms = _db.PlatformRepository.Where(a => !a.IsDeleted).Take(5).ToList(),
            };
            return View(viewModel);
        }
    }
}
