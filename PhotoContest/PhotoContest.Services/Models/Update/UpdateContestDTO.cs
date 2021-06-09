using PhotoContest.Services.ExceptionMessages;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Services.Models.Update
{
    public class UpdateContestDTO
    {
        [StringLength(50, MinimumLength = 5, ErrorMessage = Exceptions.InvalidContestInfo)]
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Phase1 { get; set; }
        public string Phase2 { get; set; }
        public string Finished { get; set; }
        public bool IsOpen { get; set; }
    }
}
