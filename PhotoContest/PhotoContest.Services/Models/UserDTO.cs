using PhotoContest.Data;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Services.Models
{
    public class UserDTO
    {
        public UserDTO(User user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Username = user.UserName;
            this.Points = user.OverallPoints;
            this.Rank = user.Rank.Name;
        }
        [Display(Name ="First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Rank { get; set; }
        public double Points { get; set; }
    } 
}
