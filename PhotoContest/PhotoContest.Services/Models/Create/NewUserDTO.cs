using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Services.Models.Create
{
    public class NewUserDTO
    {
        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string FirstName { get; set; }

        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
