using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {

        }
        public User(string email, string normalizedEmail)
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.NormalizedEmail = normalizedEmail;
            this.SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}
