using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PhotoContest.Services.Models.Create
{
    public class NewContestDTO
    {
        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Contest name should be between {1} and {2} characters.")]
        public string Name { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Phase1 { get; set; }
        [Required]
        public string Phase2 { get; set; }
        [Required]
        public string Finished { get; set; }
        [Required]
        public bool Open { get; set; }
    }
}
