using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data
{
    public class Rank
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Fan> Fans { get; set; } = new HashSet<Fan>();
    }
}
