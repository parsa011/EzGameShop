using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EzGame.Domain.Entities
{
   public class User:IdentityUser
    {
        public User()
        {
            CreatedTime = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        //relations
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
