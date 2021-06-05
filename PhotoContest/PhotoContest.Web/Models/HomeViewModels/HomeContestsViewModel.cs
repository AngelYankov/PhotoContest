using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.HomeViewModels
{
    public class HomeContestsViewModel
    {
        public List<Contest> FinishedContests { get; set; } = new List<Contest>();
        public List<ContestDTO> Phase1Contests { get; set; } = new List<ContestDTO>();
    }
}
