using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Data.Models
{
    public class JuryMember
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ContestId { get; set; }
        public Contest Contest { get; set; }
    }
}
