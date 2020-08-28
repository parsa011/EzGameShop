using System;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Common.Filters.ActionFilters;
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
        public GameController(IUnitOfWork db, IToastNotification notification, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _notification = notification;

            _fileManager = new FileManager(webHostEnvironment.WebRootPath);
        }

        #region ViewBags

        public async Task FillGenresViewBag()
        {
            ViewBag.Genres = (await _db.GenreRepository.GetAllAsync(a => !a.IsDeleted)).Select(g => new SelectListItem(g.Title, g.Id.ToString()));
        }
        public async Task FillPlatformsViewBag()
        {
            ViewBag.Platforms = (await _db.PlatformRepository.GetAllAsync(a => !a.IsDeleted)).Select(g => new SelectListItem(g.Title, g.Id.ToString()));
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewmodel = new GameIndexViewModel()
            {
                GameGenres = await _db.GameGenreRepository.GetAllAsync(a => !a.IsDeleted),
                GamePlatforms = await _db.GamePlatformRepository.GetAllAsync(a => !a.IsDeleted),
                Games = await _db.GameRepository.GetAllAsync(a => !a.IsDeleted)
            };
            return View(viewmodel);
        }
     
        [HttpGet]
        public async Task<IActionResult> AddGame()
        {
            var viewmodel = new GameCreateViewModel()
            {
                Platforms = await _db.PlatformRepository.GetAllAsync(a => !a.IsDeleted),
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
                var genre = (await _db.GenreRepository.GetAllAsync(a => a.Title == item.Title)).FirstOrDefault();
                await _db.GameGenreRepository.InsertAsync(new GameGenre()
                {
                    Game = game,
                    GameId = game.Id,
                    GenreId = genre.Id
                });
            }
            foreach (var item in model.Platforms)
            {
                var platform = (await _db.PlatformRepository.GetAllAsync(a => a.Title == item.Title)).FirstOrDefault();
                await _db.GamePlatformRepository.InsertAsync(new GamePlatform()
                {
                    Game = game,
                    GameId = game.Id,
                    PlatformId = platform.Id
                });
            }
            await _db.GameRepository.InsertAsync(game);
            await _db.SaveChangeAsync();
            _notification.AddSuccessToastMessage($"بازی {game.Title} با موفقیت ثبت شد");
            TempData["GameId"] = game.Id;
            return RedirectToAction("AddGameEdition");
        }

        [HttpGet]
        public async Task<IActionResult> EditGame(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notification.AddErrorToastMessage("بازی پیدا نشد !");
                return RedirectToAction(nameof(Index));
            }
            var game = await _db.GameRepository.GetByIdAsync(id);
            if (game == null)
                return RedirectToAction(nameof(Index));
            var model = new GameCreateViewModel()
            {
                Id = game.Id,
                Title = game.Title,
                ImageName = game.ImageName,
                ComingSoon = game.ComingSoon,
                Count = game.Count,
                Explanation = game.Explanation,
                Summary = game.Summary,
                Platforms = await _db.PlatformRepository.GetAllAsync(a => !a.IsDeleted),
                Genres = await _db.GenreRepository.GetAllAsync(a => !a.IsDeleted)
            };
            ViewBag.GameGenre = (await _db.GameGenreRepository.GetAllAsync(p => p.GameId == game.Id));
            ViewBag.GamePlatform = (await _db.GamePlatformRepository.GetAllAsync(p => p.GameId == game.Id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGame(GameCreateViewModel model, IFormFile image)
        {
            if (!ModelState.IsValid || !model.Genres.Any() || !model.Platforms.Any())
            {
                _notification.AddWarningToastMessage("مقادیر را به درستی وارد نمایید");
                return View(model);
            }
            var game = await _db.GameRepository.GetByIdAsync(model.Id);
            if (image == null)
            {
                model.Image = game.ImageName;
            }
            else
            {
                if (image.FileName != game.ImageName)
                {
                    _fileManager.DeleteImage(game.ImageName, FileManagerType.FileType.GameImage);
                    model.Image = await _fileManager.UploadImage(image,
                        FileManagerType.FileType.GameImage);    
                }
            }
            // TODO Hello :D
            await _db.GamePlatformRepository.DeleteAllRelations(game.Id);
            await _db.GameGenreRepository.DeleteAllRelations(game.Id);
            foreach (var item in model.Genres)
            {
                await _db.GameGenreRepository.InsertAsync(new GameGenre()
                {
                    Game = game,
                    GameId = game.Id,
                    GenreId = item.Id
                });
            }
            foreach (var item in model.Platforms)
            {
                await _db.GamePlatformRepository.InsertAsync(new GamePlatform()
                {
                    Game = game,
                    GameId = game.Id,
                    PlatformId = item.Id
                });
            }
            
            game.Title = model.Title;
            game.ComingSoon = model.ComingSoon;
            game.Count = model.Count;
            game.Summary = model.Summary;
            game.Explanation = model.Explanation;
            game.ImageName = model.ImageName;
            game.LastModifiedTime = DateTime.Now;
            _db.GameRepository.Update(game);
            await _db.SaveChangeAsync();
            _notification.AddSuccessToastMessage("بازی با موفقیت ویرایش شد");
            return RedirectToAction(nameof(Index));
        }

        [AjaxOnly]
        [HttpPost]
        public async Task<ActionResult> GetGameById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var Game = await _db.GameRepository.GetByIdAsync(id);
                return Json(Game);
            }
            _notification.AddErrorToastMessage("دوباره امتحان کنید");
            return Json(null);
        }


        [AjaxOnly]
        [HttpPost]
        public ActionResult DeleteGame(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var Game = _db.GameRepository.GetById(id);
                Game.IsDeleted = true;
                _db.GameRepository.Update(Game);
                _db.SaveChange();
                _notification.AddSuccessToastMessage($"پلتفرم {Game.Title} با موفقیت حذف شد.");
                return Json(Game);
            }
            _notification.AddErrorToastMessage("مقادیر نمی توانند خالی باشند");
            return Json(null);
        }

        //Get
        [HttpGet]
        public ActionResult AddGameEdition()
        {
            ViewBag.gameId = TempData["GameId"];
            return View();
        }
       
        [HttpPost]
        public async Task<ActionResult> AddGameEdition(GameAddGameEditionViewModel model,string gameId)
        {

            if (string.IsNullOrEmpty(gameId) ||!model.gameEditions.Any())
            {
                _notification.AddWarningToastMessage("مقادیر را به درستی وارد نمایید");
                return View(model);
            }
            foreach (var item in model.gameEditions)
            {
               
                var gameEdition = new GameEdition()
                {
                    GameId = gameId,
                    IsDeleted = false,
                    Price = item.Price,
                    Title = item.Title,
                    CreatedTime = DateTime.Now,
                    LastModifiedTime = DateTime.Now
                };
               await _db.GameEditionRepository.InsertAsync(gameEdition);
            }
            _db.SaveChange();
            return RedirectToAction(nameof(Index));
        }
    }
}
