using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Services.Models.Create
{
    public class NewPhotoDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ContestId { get; set; }
    }
}
