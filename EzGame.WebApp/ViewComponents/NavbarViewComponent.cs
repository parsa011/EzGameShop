using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public  IViewComponentResult InvokeAsync()
        {

            return (IViewComponentResult)View();
        }
    }
}
