using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Data.Interfaces;
using EzGame.Services.FileManager;
using EzGame.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;
        private readonly IFileManager _fileManager;
        public GameController(IUnitOfWork db, IToastNotification notification,IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _notification = notification;
            _fileManager = new FileManager(webHostEnvironment.WebRootPath);
        }
        
        #region ViewBags

        public async Task FillGenresViewBag()
        {
            ViewBag.Genres = (await _db.GenreRepository.GetAllAsync(a=>!a.IsDeleted)).Select(g => new SelectListItem(g.Title, g.Id.ToString()));
        }
        
        public async Task FillPlatformsViewBag()
        {
            ViewBag.Platforms = (await _db.PlatformRepository.GetAllAsync(a => !a.IsDeleted)).Select(g => new SelectListItem(g.Title, g.Id.ToString()));
        }

        #endregion
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> AddGame()
        {
            await FillGenresViewBag();
            await FillPlatformsViewBag();
            return View();
        }
    }
}
