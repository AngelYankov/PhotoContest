using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.PhotoViewModels
{
    public class EditPhotoViewModel
    {
        public EditPhotoViewModel()
        {

        }
        public EditPhotoViewModel(PhotoDTO photo)
        {
            this.Id = photo.Id;
            this.Title = photo.Title;
            this.Description = photo.Description;
        }
        public Guid Id { get; set; }

        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "Value for photo {0} should be between {2} and {1} characters.")]
        public string Title { get; set; }

        [Required, StringLength(30, MinimumLength = 5, ErrorMessage = "Value for photo {0} should be between {2} and {1} characters.")]
        public string Description { get; set; }
    }
}
