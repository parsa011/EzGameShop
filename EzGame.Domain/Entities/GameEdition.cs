using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Domain.Entities
{
   public class GameEdition : BaseEntity<string>
    {
        public GameEdition()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
