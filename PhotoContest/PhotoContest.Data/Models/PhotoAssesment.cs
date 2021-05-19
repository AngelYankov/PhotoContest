using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class PhotoAssesment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }
        public string Comment { get; set; }
        public int Points { get; set; }
    }
}
