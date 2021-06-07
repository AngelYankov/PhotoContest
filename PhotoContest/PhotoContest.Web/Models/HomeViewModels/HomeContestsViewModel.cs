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
        public List<PhotoDTO> PhotosFirstPlace { get; set; } = new List<PhotoDTO>();
        public List<ContestDTO> Phase1Contests { get; set; } = new List<ContestDTO>();
    }
}
