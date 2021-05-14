using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contest> Contests { get; set; } = new HashSet<Contest>();
    }
}
