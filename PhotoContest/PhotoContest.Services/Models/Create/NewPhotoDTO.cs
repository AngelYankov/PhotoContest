using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.Models.Create
{
    public class NewPhotoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public Guid UserId { get; set; }
        public Guid ContestId { get; set; }
    }
}
