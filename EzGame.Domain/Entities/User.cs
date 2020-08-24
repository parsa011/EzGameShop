using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Domain.Entities
{
   public class User:IdentityUser
    {
        public User()
        {
                
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        //relations
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
