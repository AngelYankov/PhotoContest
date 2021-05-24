using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Configurations
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Photo)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.PhotoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
