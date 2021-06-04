using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.UserViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public UserViewModel(UserDTO user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.Points = user.Points;
            this.Rank = user.Rank;
            if (user.Rank == "Junkie")
            {
                this.PointsUntilNextRank = "You need " + (51 - this.Points).ToString() + " points to reach the next rank.";
            }
            else if (user.Rank == "Enthusiast")
            {
                this.PointsUntilNextRank = "You need " + (151 - this.Points).ToString() + " points to reach the next rank.";
            }
            else if(user.Rank == "Master")
            {
                this.PointsUntilNextRank = "You need " + (1001 - this.Points).ToString() + " points to reach the next rank.";
            }
            else
            {
                this.PointsUntilNextRank = "You have reached the highest rank!";
            }
        }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Rank { get; set; }
        public double Points { get; set; }
        [Display(Name ="Points until next rank")]
        public string PointsUntilNextRank { get; set; }
    }
}
