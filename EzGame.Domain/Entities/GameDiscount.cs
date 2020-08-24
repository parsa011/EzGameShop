using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EzGame.Domain.Entities
{
   public class GameDiscount : BaseEntity<string>
    {
        public GameDiscount()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }
        public string GameId { get; set; }
        public string Percent { get; set; }
        public DateTime ExpireAt { get; set; }
        //relations
        [ForeignKey("GameId")]
        public virtual  Game Game { get; set; }
    }
}
