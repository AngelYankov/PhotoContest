using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.ContestViewModels
{
    public class EditContestViewModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        [Display(Name = "Is the contest Open?")]
        public bool IsContestOpen { get; set; }
        [Display(Name = "Phase 1 starts on: dd.mm.yy hh:mm")]
        public string Phase1 { get; set; }
        [Display(Name = "Phase 2 starts on: dd.mm.yy hh:mm")]
        public string Phase2 { get; set; }
        [Display(Name = "Contest finishes on: dd.mm.yy hh:mm")]
        public string Finished { get; set; }
    }
}
