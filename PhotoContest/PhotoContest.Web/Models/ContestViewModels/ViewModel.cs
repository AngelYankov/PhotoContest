using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.ContestViewModels
{
    public class ViewModel
    {
        public ViewModel(Contest contest)
        {
            this.Name = contest.Name;
            this.Category = contest.Category.Name;
            this.Status = contest.Status.Name;
            this.Phase1 = contest.Phase1.ToString("dd.MM.yy HH:mm");
            this.Phase2 = contest.Phase2.ToString("dd.MM.yy HH:mm");
            this.Finished = contest.Finished.ToString("dd.MM.yy HH:mm");
            if (contest.IsOpen)
                this.OpenOrInvitational = "Contest is open.";
            else
                this.OpenOrInvitational = "Contest is invitational.";
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
        [Display(Name = "Open or invitational")]
        public string OpenOrInvitational { get; set; }
    }
}

