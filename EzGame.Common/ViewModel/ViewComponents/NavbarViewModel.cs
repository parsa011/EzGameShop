using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Common.ViewModel.ViewComponents
{
    public class NavbarViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }
    }
}
