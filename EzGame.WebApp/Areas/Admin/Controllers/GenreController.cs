using System;
using System.Threading.Tasks;
using EzGame.Data.Interfaces;
using EzGame.Domain.Entities;
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
        public async Task<IActionResult> Index()
        {
            var model = await _db.GenreRepository.GetAllAsync();
            return View(model);
        }
        
        [HttpGet]
        //[Route("/Admin/PageGroups/Command/{id}/{mode}")]
        //[Route("/Admin/PageGroups/Command/{id}/{title}/{mode}")]
        public async Task<IActionResult> Command(string id, string title, string mode)
        {
            if (!string.IsNullOrEmpty(mode))
            {
                mode = mode.ToUpper();
                if (mode == "CREATE" && string.IsNullOrEmpty(title) || mode == "EDIT" && string.IsNullOrEmpty(title) || mode == "DELETE" && id == null)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            var genre = await _db.GenreRepository.GetByIdAsync(id);
            switch (mode.ToUpper())
            {
                case "CREATE":
                    await _db.GenreRepository.InsertAsync(new Genre()
                    {
                        Title = title
                    });
                    _notification.AddSuccessToastMessage($"ژانر {title} با موفقیت اضافه شده است");
                    break;
                case "EDIT":
                    genre.Title = title;
                    genre.LastModifiedTime = DateTime.Now;
                    _db.GenreRepository.Update(genre);
                    _notification.AddSuccessToastMessage($"ژانر {title} با موفقیت ویرایش شده است");
                    break;
                case "DELETE":
                    _db.GenreRepository.Delete(genre);
                    _notification.AddSuccessToastMessage($"ژانر {title} با موفقیت حذف شده است");
                    break;
            }

            await _db.SaveChangeAsync();
            return RedirectToAction("Index");
        }
    }
}