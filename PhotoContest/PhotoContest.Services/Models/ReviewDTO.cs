using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.Models
{
    public class ReviewDTO
    {
        public ReviewDTO(Review review)
        {
            this.Comment = review.Comment;
            this.Score = review.Score.ToString();
            this.Evaluator = review.Evaluator.FirstName+" "+review.Evaluator.LastName;
            this.PhotoTitle = review.Photo.Title;
        }
        public string Comment { get; set; }
        public string Score { get; set; }
        public string Evaluator { get; set; }
        public string PhotoTitle { get; set; }
    }
}
