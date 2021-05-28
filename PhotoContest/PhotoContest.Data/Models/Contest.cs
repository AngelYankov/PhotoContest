using PhotoContest.Data.Audit;
using PhotoContest.Data.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Contest : Entity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Value for contest {0} should be between {2} and {1} characters.")]
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public bool IsOpen { get; set; }
        public DateTime Phase1 { get; set; }
        public DateTime Phase2 { get; set; }
        public DateTime Finished { get; set; }
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public ICollection<UserContest> UserContests { get; set; } = new List<UserContest>();
        public ICollection<JuryMember> Juries { get; set; } = new List<JuryMember>();
    }
}
