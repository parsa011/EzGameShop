using System;

namespace EzGame.Domain.Core.Interfaces.Entities
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
        DateTime CreatedTime { get; set; }
        string CreatedBy { get; set; }
        DateTime LastModifiedTime { get; set; }
        string ModifiedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}