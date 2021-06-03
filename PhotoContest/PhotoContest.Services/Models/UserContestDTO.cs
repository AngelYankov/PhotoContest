using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.Models
{
    public class UserContestDTO
    {
        public Guid ContestId { get; set; }
        public Guid UserId { get; set; }
    }
}
