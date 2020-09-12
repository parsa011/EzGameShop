using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzGame.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EzGame.WebApp.Contorllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IToastNotification _notification;

        public ProductsController(IUnitOfWork db, IToastNotification notification)
        {
            _db = db;
            _notification = notification;
        }

        //Get 
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Games = (await _db.GameRepository.GetAllAsync(a => !a.IsDeleted));
            return View(Games);
        }

        //[HttpGet]
        //[Route("/Products/TheMostVisited")]
        //public IActionResult TheMostVisited()
        //{
        //    var Games =_db.GameRepository.Where(p=>p.);
        //    return View("Index",Games);
        //}

        [HttpGet]
        [Route("/Products/TheLatestProduct")]
        public IActionResult TheLatestProduct()
        {
            var Games = _db.GameRepository.GetAll().Where(a => !a.IsDeleted).OrderByDescending(p => p.CreatedTime);
            return View("Index", Games);
        }

        [HttpGet]
        [Route("/Products/TheCheapestProducts")]
        public IActionResult TheCheapestProducts()
        {
            var Games = _db.GameRepository.GetAll().Where(a=>!a.IsDeleted).OrderBy(p=>p.GameEditions[0].Price);
            return View("Index", Games);
        }
        
        [HttpGet]
        [Route("/Products/TheMostExpensiveProducts")]
        public IActionResult TheMostExpensiveProducts()
        {
            var Games = _db.GameRepository.GetAll().Where(a => !a.IsDeleted).OrderByDescending(p => p.GameEditions[0].Price);
            return View("Index", Games);
        }
    }
}
