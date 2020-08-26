using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Common.ViewModel.Games
{
    public class GameIndexViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<GameGenre> GameGenres { get; set; }
        public IEnumerable<GamePlatform> GamePlatforms { get; set; }
    }
}
