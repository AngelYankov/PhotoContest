using PhotoContest.Data.Audit;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Data.Models
{
    public class Review : Entity
    {
        public Guid Id { get; set; }
        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Value for review {0} should be between {2} and {1} characters.")]
        public string Comment { get; set; }
        [Required, Range(1, 10, ErrorMessage = "Value for review {0} should be between {2} and {1}.")]
        public double Score { get; set; }
        public Guid UserId { get; set; }
        public User Evaluator { get; set; }
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }
        public bool WrongCategory { get; set; }

    }
}
