using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Point
    {
        [Key]
        public Guid Id { get; set; }
        public int value { get; set; }
    }
}
