using System;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Common.ViewModel.Games;
using EzGame.Data.Interfaces;
using EzGame.Domain.Entities;
using EzGame.Domain.Enums;
using EzGame.Services.FileManager;
using EzGame.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public async Task<IActionResult>  Index()
        {
            var viewmodel = new GameIndexViewModel()
            {
                GameGenres = await _db.GameGenreRepository.GetAllAsync(),
                GamePlatforms = await _db.GamePlatformRepository.GetAllAsync(),
                Games = await _db.GameRepository.GetAllAsync()
            };
            return View(viewmodel);
        }
        
        [HttpGet]
        public async Task<IActionResult> AddGame()
        {
            var viewmodel = new GameCreateViewModel()
            {
                Platforms = await _db.PlatformRepository.GetAllAsync(a=>!a.IsDeleted),
                Genres = await _db.GenreRepository.GetAllAsync(a => !a.IsDeleted),
            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGame(GameCreateViewModel model, IFormFile image)
        {
            if (!ModelState.IsValid || image == null || !model.Genres.Any() || !model.Platforms.Any())
            {
                _notification.AddWarningToastMessage("مقادیر را به درستی وارد نمایید");
                await FillGenresViewBag();
                await FillPlatformsViewBag();
                return View(model);
            }
            
            model.ImageName = await _fileManager.UploadImage(image, FileManagerType.FileType.GameImage);
            var game = new Game()
            {
                ImageName = model.ImageName,
                IsDeleted = false,
                Count = model.Count,
                Explanation = model.Explanation,
                Summary = model.Summary,
                Title = model.Title,
                ComingSoon = model.ComingSoon,
                CreatedTime = DateTime.Now,
                LastModifiedTime = DateTime.Now,
            };
            foreach (var item in model.Genres)
            {
                await _db.GameGenreRepository.InsertAsync(new GameGenre()
                {
                    Game=game,
                    GameId = model.Id,
                    GenreId = item.Id
                });
            }
            foreach (var item in model.Platforms)
            {
                await _db.GamePlatformRepository.InsertAsync(new GamePlatform()
                {
                    Game = game,
                    GameId = model.Id,
                    PlatformId = item.Id
                });
            }
            await _db.GameRepository.InsertAsync(game);
            await _db.SaveChangeAsync();
            _notification.AddSuccessToastMessage($"بازی {game.Title} با موفقیت ثبت شد");
            return RedirectToAction(nameof(Index));
        }
    }
}
