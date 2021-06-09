using Microsoft.AspNetCore.Identity;
using PhotoContest.Data.Audit;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Data
{
    public class User : IdentityUser<Guid>, IEntity
    {
        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Value for user {0} should be between {2} and {1} characters.")]
        public string FirstName { get; set; }
        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Value for user {0} should be between {2} and {1} characters.")]
        public string LastName { get; set; }
        public Guid RankId { get; set; }
        public Rank Rank { get; set; }
        public int OverallPoints { get; set; }
        public ICollection<UserContest> UserContests { get; set; } = new List<UserContest>();
        public ICollection<JuryMember> Juries { get; set; } = new List<JuryMember>();
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
