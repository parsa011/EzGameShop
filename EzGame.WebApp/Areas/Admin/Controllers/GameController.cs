using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;
        public GameController(IUnitOfWork db, IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }

        //Get
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddGame()
        {
            return View();
        }
    }
}
