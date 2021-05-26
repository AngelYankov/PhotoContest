using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.Models
{
    public class PhotoReviewDTO
    {
        public PhotoReviewDTO(Photo photo)
        {
            this.Title = photo.Title;
            this.Description = photo.Description;
            this.PhotoUrl = photo.PhotoUrl;
            this.User = photo.User.FirstName+" "+photo.User.LastName;
            this.Contest = photo.Contest.Name;
            this.Category = photo.Contest.Category.Name;
            this.Points = photo.AllPoints;
            foreach (var review in photo.Reviews)
            {
                this.Reviews.Add((review.Comment,review.Score));
            }
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string User { get; set; }
        public string Contest { get; set; }
        public string Category { get; set; }
        public double Points { get; set; }
        public List<(string, double)> Reviews { get; set; } = new List<(string, double)>();
    }
}
