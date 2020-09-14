using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EzGame.Domain.Entities
{
    public class GameVisit:BaseEntity<string>
    {
        public GameVisit()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }

        public string Ip { get; set; }
        public string GameId { get; set; }

        //relations

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }
}
