using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Services.Models.Update
{
    public class UpdateUserDTO
    {
        [Display(Name ="First name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for user {0} should be between {2} and {1} characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for user {0} should be between {2} and {1} characters.")]
        public string LastName { get; set; }
    }
}
