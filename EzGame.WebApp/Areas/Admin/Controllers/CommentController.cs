using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;

        public CommentController(IUnitOfWork db, IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }

        //Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var getAll =(await _db.CommentRepository.GetAllAsync(a => !a.IsDeleted));
            return View(getAll);
        }
        public async Task<IActionResult> GetCommentById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = await _db.CommentRepository.GetByIdAsync(id);
                return Json(user);
            }
            _notification.AddErrorToastMessage("دوباره امتحان کنید");
            return Json(null);
        }
        public async Task<IActionResult>  DeleteComment(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var comment = await _db.CommentRepository.GetByIdAsync(id);
                comment.IsDeleted = true;
                _db.CommentRepository.Update(comment);
                _notification.AddSuccessToastMessage($"پلتفرم {comment.Id} با موفقیت حذف شد.");
                return Json(comment);
            }
            _notification.AddErrorToastMessage("مقادیر نمی توانند خالی باشند");
            return Json(null);
        }
    }
}
