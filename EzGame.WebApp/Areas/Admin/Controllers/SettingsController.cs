using EzGame.Common.ViewModel.SiteSettings;
using EzGame.Data.Interfaces;
using EzGame.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Linq;
using System.Threading.Tasks;

namespace EzGame.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class SettingsController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;

        public SettingsController(IUnitOfWork db, IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(new SiteSettingsViewModel
            {
                DollarPrice = (await _db.DollarPriceRepository.GetLastPrice())?.Value
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SiteSettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _notification.AddErrorToastMessage("لطفا مقادیر را به درستی وارد کنید");
                return RedirectToAction(nameof(Index));
            }
            if (model.DollarPrice.Value != (await _db.DollarPriceRepository.GetLastPrice())?.Value)
                await _db.DollarPriceRepository.InsertAsync(new DollarPrice
                {
                    Value = model.DollarPrice.Value
                });
            await _db.SaveChangeAsync();
            _notification.AddSuccessToastMessage("مقادیر ویرایش شد");
            return RedirectToAction(nameof(Index));
        }

    }
}
