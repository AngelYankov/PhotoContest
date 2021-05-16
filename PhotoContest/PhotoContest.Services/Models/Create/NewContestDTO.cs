using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PhotoContest.Services.Models.Create
{
    public class NewContestDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Contest name should be between {1} and {2} characters.")]
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid StatusId { get; set; }
        public DateTime Phase1 { get; set; }
        public DateTime Phase2 { get; set; }
        public DateTime Finished { get; set; }
    }
}
