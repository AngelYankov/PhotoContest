using Microsoft.AspNetCore.Identity;
using PhotoContest.Data.Audit;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhotoContest.Data
{
    public class Fan : IdentityUser<Guid>, IEntity
    {
        //[Key]
        //public Guid Id { get; set; }

        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string FirstName { get; set; }

        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string LastName { get; set; }

        //[Required, EmailAddress(ErrorMessage = "Invalid email address.")]
        //public string Email { get; set; }

        //[Required,MinLength(8, ErrorMessage = "{0} cannot be less than {1} characters.")]
        //public string Password { get; set; }
        public Guid RankId { get; set; }
        public Rank Rank { get; set; }
        public int CurrentScore { get; set; }
        public Dictionary<Guid,int> ContestScores { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public List<FanContest> FanContests { get; set; } = new List<FanContest>();
        public List<Jury> Jurors { get; set; } = new List<Jury>();
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
