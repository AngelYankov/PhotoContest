using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Data.Models;

namespace PhotoContest.Data.Configurations
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(r => r.Evaluator)
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
