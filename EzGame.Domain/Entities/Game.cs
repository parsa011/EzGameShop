using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Domain.Entities
{
    public class Game : BaseEntity<string>
    {
        public Game()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public string Summary { get; set; }
        public int Count { get; set; }
        public bool ComingSoon { get; set; }
        //relations
        public virtual IEnumerable<Comment> Comments { get; set; }
        public virtual IEnumerable<GameDiscount> GameDiscount { get; set; }
        public virtual IEnumerable<GameGenre> GameGenre { get; set; }
        public virtual IEnumerable<GameEdition> GameEditions { get; set; }
        public virtual IEnumerable<GamePlatform> GamePlatform { get; set; }


    }
}
