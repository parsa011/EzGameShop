using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EzGame.Domain.Entities
{
   public class GamePlatform : BaseEntity<string>
    {
        public GamePlatform()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }
        public string GameId { get; set; }
        public string PlatformId { get; set; }
        //relations
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        [ForeignKey("PlatformId")]
        public virtual Platform Platform { get; set; }

    }
}
