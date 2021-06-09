using PhotoContest.Data.Models;
using System;
using System.Text.Json.Serialization;

namespace PhotoContest.Services.Models
{
    public class ReviewDTO
    {
        public ReviewDTO(Review review)
        {
            this.Id = review.Id;
            this.Comment = review.Comment;
            this.Score = review.Score;
            this.Evaluator = review.Evaluator.FirstName+" "+review.Evaluator.LastName;
            this.PhotoTitle = review.Photo.Title;
        }
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public double Score { get; set; }
        public string Evaluator { get; set; }
        public string PhotoTitle { get; set; }
    }
}
