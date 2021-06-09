using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Services.Models.SecurityModels
{
    public class TokenRequestModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
