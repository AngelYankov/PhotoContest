using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Services.Models.Update
{
    public class UpdatePhotoDTO
    {
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Title { get; set; }

        [StringLength(30, MinimumLength = 5, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }
}
