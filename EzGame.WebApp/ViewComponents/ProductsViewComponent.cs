using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzGame.WebApp.ViewComponents
{
    public class ProductsViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _db;
        public ProductsViewComponent(IUnitOfWork db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

          var  Game = (await _db.GameRepository.GetAllAsync(a => !a.IsDeleted));
            return (IViewComponentResult)View("Product", Game);
        }
    }
}
