using PhotoContest.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Status 
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contest> Contests { get; set; } = new List<Contest>();
    }
}
