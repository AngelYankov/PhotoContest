using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PhotoContest.Services.Models
{
    public class ContestDTO
    {
        public ContestDTO(Contest contest)
        {
            this.Name = contest.Name;
            this.Category = contest.Category.Name;
            this.Status = contest.Status.Name;
            this.Phase1 = contest.Phase1.ToString();
            this.Phase2 = contest.Phase2.ToString();
            this.Finished = contest.Finished.ToString();
        }
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Phase1 { get; set; }
        public string Phase2 { get; set; }
        public string Finished { get; set; }
    }
}
