using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Services.Models.Create
{
    public class NewPhotoDTO
    {
        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Title { get; set; }
        [Required, StringLength(30, MinimumLength = 5, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Description { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ContestId { get; set; }
    }
}
