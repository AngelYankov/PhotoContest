using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Services.Models.Update
{
    public class UpdateContestDTO
    {
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Contest name should be between {1} and {2} characters.")]
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string StatusName { get; set; }
        public string Phase1 { get; set; }
        public string Phase2 { get; set; }
        public string Finished { get; set; }
        public bool IsOpen { get; set; }
    }
}
