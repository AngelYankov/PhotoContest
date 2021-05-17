using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Jury
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ContestId { get; set; }
        public Contest Contest { get; set; }
    }
}
