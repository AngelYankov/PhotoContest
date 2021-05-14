using PhotoContest.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Organizer : Entity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string FirstName { get; set; }

        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string LastName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required, MinLength(8, ErrorMessage = "{0} cannot be less than {1} characters.")]
        public string Password { get; set; }
    }
}
