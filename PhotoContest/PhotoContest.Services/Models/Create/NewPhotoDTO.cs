using PhotoContest.Services.ExceptionMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Services.Models.Create
{
    public class NewPhotoDTO
    {
        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = Exceptions.InvalidPhotoInfo)]
        public string Title { get; set; }
        [Required, StringLength(30, MinimumLength = 5, ErrorMessage = Exceptions.InvalidPhotoInfo)]
        public string Description { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        [Required]
        public string ContestName{ get; set; }
    }
}
