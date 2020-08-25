using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EzGame.Common.Filters.ActionFilters;
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

        //Get
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var getAll = await _db.PlatformRepository.GetAllAsync(a=>!a.IsDeleted);
            return View(getAll);
        }
        
        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> AddPlatform(string title,string isDeleted,string logo)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(isDeleted) && !string.IsNullOrEmpty(logo))
            {
                var platform = new Platform()
                {
                    Title = title,
                    Logo = logo,
                    IsDeleted = isDeleted == "حذف شده",
                    CreatedTime=DateTime.Now,
                    LastModifiedTime=DateTime.Now
                };
                await _db.PlatformRepository.InsertAsync(platform);
                 _db.SaveChange();
                _notification.AddSuccessToastMessage($"پلتفرم {title} با موفقیت ساخته شد.");
                return Json(platform);
            }
            _notification.AddErrorToastMessage("نام و وضعیت نمی تواند خالی باشد");
            return Json(null);
        }
        
        [AjaxOnly]
        [HttpPost]
        public async Task<ActionResult> GetPlatformById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var platform = await _db.PlatformRepository.GetByIdAsync(id);
                return Json(platform);
            }
            _notification.AddErrorToastMessage("دوباره امتحان کنید");
            return Json(null);
        }

        [AjaxOnly]
        [HttpPost]
        public ActionResult UpdatePlatform(string id, string title, string isDeleted,string logo)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(isDeleted) && !string.IsNullOrEmpty(logo))
            {
                var platform = _db.PlatformRepository.GetById(id);
                platform.IsDeleted = isDeleted == "حذف شده";
                platform.Title = title;
                platform.Logo = logo;
                platform.LastModifiedTime = DateTime.Now;
                _db.PlatformRepository.Update(platform);
                _db.SaveChange();
                _notification.AddSuccessToastMessage($"پلتفرم {title} با موفقیت ویرایش شد.");
                return Json(platform);

            }
            _notification.AddErrorToastMessage("مقادیر نمی توانند خالی باشند");
            return Json(null);
        }

        [AjaxOnly]
        [HttpPost]
        public ActionResult DeletePlatform(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var platform = _db.PlatformRepository.GetById(id);
                platform.IsDeleted = true;
                _db.PlatformRepository.Update(platform);
                _db.SaveChange();
                _notification.AddSuccessToastMessage($"پلتفرم {platform.Title} با موفقیت حذف شد.");
                return Json(platform);
            }
            _notification.AddErrorToastMessage("مقادیر نمی توانند خالی باشند");
            return Json(null);
        }
    }
}
