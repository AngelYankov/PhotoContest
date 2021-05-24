using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Configurations
{
    public class PhotoRatingConfig : IEntityTypeConfiguration<PhotoRating>
    {
        public void Configure(EntityTypeBuilder<PhotoRating> builder)
        {
            builder.HasKey(pr => new { pr.PhotoId, pr.UserId });

           /* builder.HasOne(pr => pr.User)
                .WithMany(u => u.PhotoRatings)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pr => pr.Photo)
                .WithMany(p => p.PhotoRatings)
                .HasForeignKey(pr => pr.PhotoId)
                .OnDelete(DeleteBehavior.NoAction);*/
        }
    }
}
