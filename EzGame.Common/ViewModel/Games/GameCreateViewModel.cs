using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Common.ViewModel.Games
{
   public class GameCreateViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }

    }
}
