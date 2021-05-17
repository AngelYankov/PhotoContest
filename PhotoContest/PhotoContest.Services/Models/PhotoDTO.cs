using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.Models
{
    public class PhotoDTO
    {
        public PhotoDTO(Photo photo)
        {
            this.Id = photo.Id;
            this.Title = photo.Title;
            this.Description = photo.Description;
            this.PhotoUrl = photo.PhotoUrl;
            this.User = photo.User.FirstName + photo.User.LastName;
            this.Contest = photo.Contest.Name;
            this.Category = photo.Contest.Category.Name;
            int totalPoints = 0;
            for (int i = 0; i < photo.Points.Count; i++)
            {
                totalPoints += photo.Points[i].value;
            }
            this.Points = totalPoints;
        }
        public Guid Id{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string User { get; set; }
        public string Contest { get; set; }
        public string Category { get; set; }
        public int Points { get; set; }
    }
}
