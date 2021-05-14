﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Data
{
    public class Rank
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Fan> Fans { get; set; } = new HashSet<Fan>();
    }
}
