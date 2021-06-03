using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models
{
    public class ReviewViewModel
    {
        public ReviewViewModel()
        {

        }
        public ReviewViewModel(ReviewDTO review)
        {
            this.Id = review.Id;
            this.Comment = review.Comment;
            this.Score = review.Score;
            this.Evaluator = review.Evaluator;
            this.PhotoTitle = review.PhotoTitle;
        }
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public double Score { get; set; }
        public string Evaluator { get; set; }
        public string PhotoTitle { get; set; }
    }
}
