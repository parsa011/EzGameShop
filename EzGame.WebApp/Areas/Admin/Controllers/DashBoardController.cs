using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Common.ViewModel.DashBoard;
using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class DashBoardController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;

        public DashBoardController(IUnitOfWork db, IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.GameCount = await _db.GameRepository.CountAsync();
            ViewBag.UserCount = await _db.UserRepository.CountAsync();
            ViewBag.CommentCount = await _db.CommentRepository.CountAsync();
            var viewmodel = new DashBoardIndexViewModel() {
                Games = _db.GameRepository.Where(p => p.CreatedTime.Date == DateTime.Now.Date && !p.IsDeleted).ToList(),
                Comments=_db.CommentRepository.Where(p=> p.CreatedTime.Date == DateTime.Now.Date && !p.IsDeleted).ToList(),
                Users= _db.UserRepository.Where(p => p.CreatedTime.Date == DateTime.Now.Date&&!p.IsDeleted).ToList(),
            };
            return View(viewmodel);
        }
    }
}
