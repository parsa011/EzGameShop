using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Domain.Entities
{
   public class Genre: BaseEntity<string>
    {
        public Genre()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }
        public string Title { get; set; }
        //relations
        public virtual IEnumerable<GameGenre> GameGenres { get; set; }

    }
}
