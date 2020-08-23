using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EzGame.Domain.Core.Interfaces.Entities;

namespace EzGame.Domain.Core.Services.Entities
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get;set; }
        public DateTime CreatedTime { get;set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedTime { get;set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}