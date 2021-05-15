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

        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Contest name should be between {1} and {2} characters.")]
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        [ValidateTimeNotInThePast(ErrorMessage = "Date cannot be in the past.")]
        public DateTime Phase1 { get; set; }
        public DateTime Phase2 { get; set; }
        public DateTime Finished { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public List<FanContest> FanContests { get; set; } = new List<FanContest>();
        public List<Jury> Jurors { get; set; } = new List<Jury>();

    }
}
