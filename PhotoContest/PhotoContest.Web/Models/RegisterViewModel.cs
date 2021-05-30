using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Web.Models
{
    public class RegisterViewModel
    {
		[Required]
		[EmailAddress]
		[Display(Name = "E-mail")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "The password must be between 3 and 20 characters long")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirm password fields must match")]
		public string ConfirmPassword { get; set; }
	}
}