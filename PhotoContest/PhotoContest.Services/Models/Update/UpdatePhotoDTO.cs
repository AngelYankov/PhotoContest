using PhotoContest.Services.ExceptionMessages;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Services.Models.Update
{
    public class UpdatePhotoDTO
    {
        [StringLength(15, MinimumLength = 3, ErrorMessage = Exceptions.InvalidPhotoInfo)]
        public string Title { get; set; }

        [StringLength(30, MinimumLength = 5, ErrorMessage = Exceptions.InvalidPhotoInfo)]
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }
}
