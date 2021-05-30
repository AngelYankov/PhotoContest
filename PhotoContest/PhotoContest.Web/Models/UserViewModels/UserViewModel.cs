using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.UserViewModels
{
    public class UserViewModel
    {
        public UserViewModel(UserDTO user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.Points = user.Points;
            this.Rank = user.Rank;
        }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Rank { get; set; }
        public double Points { get; set; }
    }
}
