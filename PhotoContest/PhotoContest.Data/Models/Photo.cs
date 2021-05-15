using PhotoContest.Data.Audit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Photo : Entity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string photoUrl { get; set; }
        public Guid FanId { get; set; }
        public Fan Fan { get; set; }
        public Guid ContestId { get; set; }
        public Contest Contest { get; set; }
        public List<int> Points { get; set; } = new List<int>();
    }
}
