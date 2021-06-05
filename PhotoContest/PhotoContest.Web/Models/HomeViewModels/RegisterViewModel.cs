using PhotoContest.Services.ExceptionMessages;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Web.Models
{
    public class RegisterViewModel
    {
		[Required, StringLength(20, MinimumLength = 2, ErrorMessage = Exceptions.InvalidUserInfo)]
		public string FirstName { get; set; }

		[Required, StringLength(20, MinimumLength = 2, ErrorMessage = Exceptions.InvalidUserInfo)]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "E-mail")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = Exceptions.InvalidPassword)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = Exceptions.NotMatchingPassword)]
		public string ConfirmPassword { get; set; }
	}
}