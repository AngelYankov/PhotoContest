using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public double Score { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }

    }
}
