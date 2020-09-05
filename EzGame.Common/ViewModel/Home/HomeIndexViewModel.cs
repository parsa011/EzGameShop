using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Common.ViewModel.Home
{
   public class HomeIndexViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Platform> platforms { get; set; }
    }
}
