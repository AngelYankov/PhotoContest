using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Services.Models.SecurityModels
{
    public class AddRoleModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
