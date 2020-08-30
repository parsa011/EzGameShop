using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EzGame.Common.ViewModel.Account;
using EzGame.Common.ViewModel.Settings;
using EzGame.Domain.Entities;
using EzGame.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MPS.Services.Services.Senders;
using NToastNotify;

namespace EzGame.WebApp.Contorllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IToastNotification _notification;
        private readonly ILogger<AccountController> _logger;
        private readonly ISender _sender;
        public AccountController(UserManager<User> userManager,ILogger<AccountController> logger,
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
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                _notification.AddWarningToastMessage("لطفا مقادیر را به درستی وارد نمایید");
                return View(model);
            }
             var user = new User()
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var messageUrl = Url.Action("ConfirmEmail", "Account",
                    new { username = user.UserName, token = emailConfirmationToken },
                    Request.Scheme);
                var message = "برای تایید ایمیل بر روی این " + $"<a href='{messageUrl}'>کلیک</a> کنید";
                var emailRes = await _sender.SendEmailAsync(user.Email, "تایید ایمیل | ایزی گیم", message, true);
                if (emailRes)
                {
                    _notification.AddSuccessToastMessage("ایمیل خود را تایید کنید | با موفقیت ثبت نام کردید");
                }
                else
                {
                    _logger.Log(logLevel: LogLevel.Error, "error in sending email for activating account");
                    _notification.AddErrorToastMessage("در تایید کردن ایمیل مشکلی پیش امده است");
                }

                return RedirectToAction("Index", "Home");
            }
            SetErrors(result);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            _notification.RemoveAll();
            if (result.Succeeded)
            {
                _notification.AddSuccessToastMessage("میتوانید وارد شوید | ایمیل شما با موفقیت تایید شد");
            }
            else
            {
                _notification.AddErrorToastMessage("در تایید کردن ایمیل مشکلی پیش امده است");
            }

            return Redirect("/");
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var res = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                if (res.Succeeded)
                {
                    _notification.AddSuccessToastMessage("با موفقیت وارد شدید");
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                if (res.IsLockedOut)
                {
                    ModelState.AddModelError("","اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیقه قفل شده است");
                    return View(model);
                }
                ModelState.AddModelError("", "مقادیر را به درستی وارد کنید");
            }
            else
            {
                ModelState.AddModelError("", "چنین کاربری پیدا نشد");
            }

            return View(model);
        }

        
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirect = Url.Action("ExternalLoginCallBack", "Auth", new { returnUrl });
            var props = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirect);
            return new ChallengeResult(provider, props);
        }

        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null, string remoteError = null)
        {
            returnUrl =
                (returnUrl != null && Url.IsLocalUrl(returnUrl)) ? returnUrl : Url.Content("~/");

            var loginViewModel = new AccountLoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalsLogin = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Error : {remoteError}");
                return View("Login", loginViewModel);
            }

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                ModelState.AddModelError("ErrorLoadingExternalLoginInfo", "مشکلی پیش آمد");
                return View("Login", loginViewModel);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey, false, true);

            if (signInResult.Succeeded)
            {
                _notification.AddSuccessToastMessage("با موفقیت وارد شدید");
                return Redirect(returnUrl);
            }

            var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var userName = email.Split('@')[0];
                    user = new User()
                    {
                        UserName = (userName.Length <= 10 ? userName : userName.Substring(0, 10)),
                        Email = email,
                        EmailConfirmed = true
                    };

                    await _userManager.CreateAsync(user);
                    var passWord = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var identityResult = await _userManager.ResetPasswordAsync(user, token, passWord);
                    if (identityResult.Succeeded)
                    {
                        await _sender.SendEmailAsync(user.Email, "ثبت نام | ایزی گیم",
                        $"با موفقیت ثبت نام کردید و وارد شدید . پسورد شما به صورت خودکار {passWord} قرار داده شده است.");
                    }
                }

                await _userManager.AddLoginAsync(user, externalLoginInfo);
                await _signInManager.SignInAsync(user, false);
                _notification.AddSuccessToastMessage("با موفقیت وارد شدید");
                return Redirect(returnUrl);
            }

            ViewBag.ErrorTitle = "لطفا با بخش پشتیبانی تماس بگیرید";
            ViewBag.ErrorMessage = $"دریافت کرد {externalLoginInfo.LoginProvider} نمیتوان اطلاعاتی از";
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

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
