using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.PhotoViewModels
{
    public class PhotoViewModel
    {
        public PhotoViewModel(PhotoDTO photo)
        {
            this.Id = photo.Id;
            this.Title = photo.Title;
            this.Description = photo.Description;
            this.PhotoUrl = photo.PhotoUrl;
            this.User = photo.User;
            this.Contest = photo.Contest;
            this.Category = photo.Category;
            this.Points = photo.Points;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name ="Picture")]
        public string PhotoUrl { get; set; }
        public string User { get; set; }
        public string Contest { get; set; }
        public string Category { get; set; }
        public double Points { get; set; }
    }
}
