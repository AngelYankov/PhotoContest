using PhotoContest.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Photo : Entity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(20, MinimumLength = 3, ErrorMessage = "Value for photo {0} should be between {2} and {1} characters.")]
        public string Title { get; set; }

        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Value for photo {0} should be between {2} and {1} characters.")]
        public string Description { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ContestId { get; set; }
        public Contest Contest { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public double AllPoints { get; set; }
        public bool IsInWrongCategory { get; set; }
    }
}
