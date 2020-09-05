using EzGame.Common.ViewModel.ViewComponents;
using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;using System;

using System.Threading.Tasks;

namespace EzGame.WebApp.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _db;

        public NavbarViewComponent(IUnitOfWork db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new NavbarViewModel
            {
                Genres = await _db.GenreRepository.GetAllAsync(),
                Platforms = await _db.PlatformRepository.GetAllAsync()
            };
            return (IViewComponentResult)View("Navbar",model);
        }
    }
}
