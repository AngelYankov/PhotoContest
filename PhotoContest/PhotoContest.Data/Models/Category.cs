using PhotoContest.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Data.Models
{
    public class Category : Entity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contest> Contests { get; set; } = new HashSet<Contest>();
    }
}
