﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.ContestViewModels
{
    public class CreateViewModel
    {
        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Contest name should be between {1} and {2} characters.")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Phase 1")]
        public string Phase1 { get; set; }
        [Required]
        [Display(Name = "Phase 2")]
        public string Phase2 { get; set; }
        [Required]
        public string Finished { get; set; }
        [Required]
        [Display(Name = "Is the contest Open?")]
        public bool IsOpen { get; set; }
    }
}
