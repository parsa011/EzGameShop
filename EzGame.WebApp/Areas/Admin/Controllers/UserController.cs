using System;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Common.Filters.ActionFilters;
using EzGame.Common.ViewModel.Account;
using EzGame.Common.ViewModel.Settings;
using EzGame.Common.ViewModel.Users;
using EzGame.Domain.Entities;
using EzGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MPS.Services.Services.Senders;
using NToastNotify;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IToastNotification _notification;
        private readonly ILogger<UserController> _logger;
        private readonly ISender _sender;
        public UserController(UserManager<User> userManager,ILogger<UserController> logger,
           SignInManager<User> signInManager,IToastNotification notification, IOptionsSnapshot<SiteSettings> emailSettings)
        {
           _userManager = userManager;
           _notification = notification;
           _signInManager = signInManager;
           _logger = logger;
           _sender = new EmailSender(emailSettings);
        }

        //Get
        [HttpGet]
        public IActionResult Index()
        {
            var getAll = _userManager.Users.Where(a => !a.IsDeleted).ToList();
            return View(getAll);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notification.AddErrorToastMessage("کاربر پیدا نشد !");
                return RedirectToAction(nameof(Index));
            }
            var user = (await _userManager.FindByIdAsync(id));
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(new UserEditViewModel{
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                userId = user.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserEditViewModel model)
        {
            if (string.IsNullOrEmpty(model.userId))
            {
                _notification.AddWarningToastMessage("مقادیر را به درستی وارد نمایید");
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.userId);
            if (model.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPassResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (!resetPassResult.Succeeded)
                {
                    SetErrors(resetPassResult);
                    return View(model);
                }
            }

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            await _userManager.UpdateAsync(user);
            
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = await _userManager.FindByIdAsync(id);
                return Json(user);
            }
            _notification.AddErrorToastMessage("دوباره امتحان کنید");
            return Json(null);
        }

        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = await _userManager.FindByIdAsync(id);
                user.IsDeleted = true;
                await _userManager.UpdateAsync(user);
                _notification.AddSuccessToastMessage($"پلتفرم {user.UserName} با موفقیت حذف شد.");
                return Json(user);
            }
            _notification.AddErrorToastMessage("مقادیر نمی توانند خالی باشند");
            return Json(null);
        }

        [HttpGet]
        public async Task<IActionResult> AddToRole(string userId)
        {
            if(string.IsNullOrEmpty(userId))
                return RedirectToAction(nameof(Index));
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
                return RedirectToAction(nameof(Index));
           
            await _userManager.AddToRoleAsync(user,"Admin");
            _notification.AddSuccessToastMessage("کاربر به ادمین تبدیل شد");
            
            return RedirectToAction(nameof(Index));
        }

        #region Helpers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }

            return Json("ایمیل وارد شده از قبل موجود است");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsUserNameInUse(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Json(true);
            }

            return Json("این نام کاربری از قبل موجود است");
        }

        public void SetErrors(IdentityResult results)
        {
            foreach (var item in results.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }

        #endregion
    }
}
