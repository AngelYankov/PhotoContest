using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Contest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime Phase1 { get; set; }
        public DateTime Phase2 { get; set; }
        public DateTime Finished { get; set; }
    }
}
