using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Common.ViewModel.Account;
using EzGame.Data.Interfaces;
using EzGame.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IToastNotification _notification;
        private readonly UserManager<User> _userManager;

        public UserController( IToastNotification notification,UserManager<User> userManager)
        {
            _userManager = userManager;
            _notification = notification;
        }

        //Get
        [HttpGet]
        public IActionResult Index()
        {
            var getAll =_userManager.Users.ToList();
            return View(getAll);
        }

        [HttpGet]
        public async Task<IAsyncResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notification.AddErrorToastMessage("کاربر پیدا نشد !");
                return (IAsyncResult)RedirectToAction(nameof(Index));
            }
            var getById = await _userManager.FindByIdAsync(id);
            if (getById == null)
            {
                return (IAsyncResult)RedirectToAction(nameof(Index));
            }
            return (IAsyncResult)View(getById);
        }

        [HttpPost]
        public IAsyncResult EditUser(UserEditUserViewModel model)
        {
            if (string.IsNullOrEmpty(model.userId))
            {
                _notification.AddWarningToastMessage("مقادیر را به درستی وارد نمایید");
                return (IAsyncResult)View(model);
            }
            var user = _userManager.FindByIdAsync(model.userId);
            if (model.Password != null)
            {
                // اینجا پسورد رو درست کن
             
            }

            user.Result.Email = model.Email;
            user.Result.UserName = model.UserName;
            user.Result.FirstName = model.FirstName;
            user.Result.LastName = model.LastName;
            _userManager.UpdateAsync(user.Result);

            return (IAsyncResult)RedirectToAction(nameof(Index));
        }

    }
}
