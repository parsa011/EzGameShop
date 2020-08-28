using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EzGame.Common.ViewModel.Games
{
   public class GameAddGameEditionViewModel
    {
        public IEnumerable<GameEdition> gameEditions { get; set; }
    }
}
