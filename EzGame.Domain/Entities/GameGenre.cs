using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EzGame.Domain.Entities
{
   public class GameGenre : BaseEntity<string>
    {
        public GameGenre()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }
        public string GameId { get; set; }
        public string GenreId { get; set; }
        //relations
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
