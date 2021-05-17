using PhotoContest.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Photo : Entity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ContestId { get; set; }
        public Contest Contest { get; set; }
        [NotMapped]
        public List<int> Points { get; set; } = new List<int>();
    }
}
