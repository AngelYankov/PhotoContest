using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.ContestViewModels
{
    public class ViewModel
    {
        public ViewModel(ContestDTO contestDTO)
        {
            this.Name = contestDTO.Name;
            this.Category = contestDTO.Category;
            this.Status = contestDTO.Status;
            this.Phase1 = contestDTO.Phase1;
            this.Phase2 = contestDTO.Phase2;
            this.Finished = contestDTO.Finished;
            this.OpenOrInvitational = contestDTO.OpenOrInvitational;
            if (this.Status == "Phase 1")
            {
                this.TimeUntilNextPhase = Phase2;
            }
            if (this.Status == "Phase 2")
            {
                this.TimeUntilNextPhase = Finished;
            }
            if(this.Status == "Finished")
            {
                this.TimeUntilNextPhase = "Contest is in the final phase.";
            }
        }
        public string Name { get; set; }
        public string Category { get; set; }
        [Display(Name = "Current phase")]
        public string Status { get; set; }
        [Display(Name = "Phase 1")]
        public string Phase1 { get; set; }
        [Display(Name = "Phase 2")]
        public string Phase2 { get; set; }
        [Display(Name = "Finished")]
        public string Finished { get; set; }
        [Display(Name = "Next phase begins on:")]
        public string TimeUntilNextPhase { get; set; }

        [Display(Name = "Тype")]
        public string OpenOrInvitational { get; set; }
        public List<UserContest> AllUserContests { get; set; } = new List<UserContest>();
        public List<PhotoDTO> AllPhotos { get; set; } = new List<PhotoDTO>();
        public string Filter { get; set; }
    }
}

