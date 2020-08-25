using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Data.Context;
using EzGame.Data.Interfaces;
using EzGame.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlatformController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;
        public PlatformController(IUnitOfWork db,IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }
        public async Task<ActionResult> Index()
        {
            var getAll = await _db.PlatformRepository.GetAllAsync();
            return View(getAll);
        }
        public async Task<ActionResult> AddPlatform(string Title,string isDeleted,string Logo)
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(isDeleted) && !string.IsNullOrEmpty(Logo))
            {
                var platform = new Platform()
                {
                    Title = Title,
                    Logo = Logo,
                    IsDeleted = isDeleted == "حذف شده" ? true : false,
                    CreatedTime=DateTime.Now,
                    LastModifiedTime=DateTime.Now
                };
                await _db.PlatformRepository.InsertAsync(platform);
                 _db.SaveChange();
                _notification.AddSuccessToastMessage($"پلتفرم {Title} با موفقیت ساخته شد.");
                return Json(platform);
            }
                _notification.AddErrorToastMessage("نام و وعضیت نمی تواند خالی باشد");
            return Json(null);
        }
        public async Task<ActionResult> GetPlatformByID(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var platform = await _db.PlatformRepository.GetByIdAsync(id);
                return Json(platform);
            }
            _notification.AddErrorToastMessage("دوباره امتحان کنید");
            return Json(null);
        }
        public ActionResult UpdateGroup(string ID, string Title, string isDeleted,string logo)
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(isDeleted) && !string.IsNullOrEmpty(logo))
            {
                var platform = _db.PlatformRepository.GetById(ID);
                platform.IsDeleted = isDeleted == "حذف شده" ? true : false;
                platform.Title = Title;
                platform.Logo = logo;
                platform.LastModifiedTime = DateTime.Now;
                _db.PlatformRepository.Update(platform);
                _db.SaveChange();
                _notification.AddSuccessToastMessage($"پلتفرم {Title} با موفقیت ویرایش شد.");
                return Json(platform);

            }
            _notification.AddErrorToastMessage("مقادیر نمی توانند خالی باشند");
            return Json(null);
        }
        public ActionResult DeleteGroup(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var platform = _db.PlatformRepository.GetById(id);
                _db.PlatformRepository.Delete(platform);
                _db.SaveChange();
                _notification.AddSuccessToastMessage($"پلتفرم {platform.Title} با موفقیت حذف شد.");
                return Json(platform);
            }
            _notification.AddErrorToastMessage("مقادیر نمی توانند خالی باشند");
            return Json(null);
        }
    }
}
