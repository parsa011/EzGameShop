using EzGame.Domain.Core.Services;
using EzGame.Domain.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Domain.Entities
{
   public class DollarPrice : BaseEntity<string>
    {
        public DollarPrice()
        {
            CreatedTime = DateTime.Now;
            Id = IdGenerator.NewGuid();
            LastModifiedTime = DateTime.Now;
        }
        public decimal Value { get; set; }
    }
}
