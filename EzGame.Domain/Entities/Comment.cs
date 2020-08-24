using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EzGame.Domain.Entities
{
   public class Comment : BaseEntity<string>
    {
        public Comment()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }
        public string UserId { get; set; }
        public string GameId { get; set; }
        public string Text { get; set; }
        //ralation
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }
}
