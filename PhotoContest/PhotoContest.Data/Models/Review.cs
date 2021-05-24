﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Comment { get; set; }
        [Required, Range(1, 10, ErrorMessage = "Value for {0} should be between {1} and {2}.")]
        public double Score { get; set; }
        public Guid UserId { get; set; }
        public User Evaluator { get; set; }
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool WrongCategory { get; set; }

    }
}
