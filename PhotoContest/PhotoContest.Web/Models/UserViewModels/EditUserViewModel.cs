using PhotoContest.Services.ExceptionMessages;
using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.UserViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {

        }
        public EditUserViewModel(UserDTO userDTO)
        {
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            Username = userDTO.Username;
        }
        [Display(Name = "First name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = Exceptions.InvalidUserInfo)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = Exceptions.InvalidUserInfo)]
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
