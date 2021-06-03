using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.ReviewViewModels
{
    public class CreateReviewViewModel
    {
        [Required]
        public Guid PhotoId { get; set; }
        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Value for photo {0} should be between {2} and {1} characters.")]
        public string Comment { get; set; }
        [Required, Range(1, 10, ErrorMessage = "Value for photo {0} should be between {2} and {1}.")]
        public double Score { get; set; }
        public bool WrongCategory { get; set; }
        public string PhotoUrl { get; set; }
    }
}
