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

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IToastNotification _notification;
        private readonly ILogger<UsersController> _logger;
        private readonly ISender _sender;
        public UsersController(UserManager<User> userManager,ILogger<UsersController> logger,
           SignInManager<User> signInManager,IToastNotification notification, IOptionsSnapshot<SiteSettings> emailSettings)
        {
           _userManager = userManager;
           _notification = notification;
           _signInManager = signInManager;
           _logger = logger;
           _sender = new EmailSender(emailSettings);
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
