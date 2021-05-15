using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Models
{
    public class Jury
    {
        public Guid Id { get; set; }
        public Guid FanId { get; set; }
        public Fan Fan { get; set; }
        public Guid ContestId { get; set; }
        public Contest Contest { get; set; }
    }
}
