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
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }
        public int Value { get; set; }
        //TODO create method with value(int)
    }
}
