using PhotoContest.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.Models
{
    public class UserDTO
    {
        public UserDTO(User user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            //this.Rank = user.Rank.Name;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rank { get; set; }
    }
}
