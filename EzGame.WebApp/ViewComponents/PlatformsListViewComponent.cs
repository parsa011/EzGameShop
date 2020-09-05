using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzGame.WebApp.ViewComponents
{
    public class PlatformsListViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _db;
        public PlatformsListViewComponent(IUnitOfWork db)
        {
            _db = db;
        }
        public  IViewComponentResult Invoke()
        {
           var platforms = _db.PlatformRepository.Where(a => !a.IsDeleted).Take(5).ToList();
            return (IViewComponentResult)View("PlatformList", platforms);

        }
    }
}
