using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

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
            this.User = photo.User.FirstName+" "+photo.User.LastName;
            this.Username = photo.User.UserName;
            this.Contest = photo.Contest.Name;
            this.ContestStatus = photo.Contest.Status.Name;
            this.Category = photo.Contest.Category.Name;
            this.Points = photo.AllPoints;
        }
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string User { get; set; }
        [JsonIgnore]
        public string Username { get; set; }
        public string Contest { get; set; }
        [JsonIgnore]
        public string ContestStatus { get; set; }
        public string Category { get; set; }
        public double Points { get; set; }
    }
}
