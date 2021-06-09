using PhotoContest.Services.ExceptionMessages;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Services.Models.Update
{
    public class UpdateUserDTO
    {
        [Display(Name ="First name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = Exceptions.InvalidUserInfo)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = Exceptions.InvalidUserInfo)]
        public string LastName { get; set; }
    }
}
