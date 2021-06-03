using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.Models
{
    public class UserContestDTO
    {
        private readonly IUserContestService userContestService;

        public UserContestDTO(IUserContestService userContestService)
        {
            this.userContestService = userContestService;
        }
        public Guid ContestId { get; set; }
        public Guid UserId { get; set; }
    }
}
