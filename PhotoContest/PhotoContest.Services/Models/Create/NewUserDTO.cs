using PhotoContest.Services.ExceptionMessages;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Services.Models.Create
{
    public class NewUserDTO
    {
        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = Exceptions.InvalidUserInfo)]
        public string FirstName { get; set; }

        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = Exceptions.InvalidUserInfo)]
        public string LastName { get; set; }
        
        [Required,EmailAddress,Display(Name ="Email address")]
        public string Email { get; set; }

        [Required, EmailAddress]
        [Display(Name = "Confirm email address")]
        [Compare(nameof(Email), ErrorMessage = "The email does not match.")]
        public string EmailConfirmed { get; set; }

        [Required,MinLength(8),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
