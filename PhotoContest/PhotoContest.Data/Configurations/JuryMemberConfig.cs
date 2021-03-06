using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Data.Models;

namespace PhotoContest.Data.Configurations
{
    public class JuryMemberConfig : IEntityTypeConfiguration<JuryMember>
    {
        public void Configure(EntityTypeBuilder<JuryMember> builder)
        {
            builder.HasKey(j => new { j.UserId, j.ContestId });

            builder.HasOne(j => j.User)
                .WithMany(u => u.Juries)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(j => j.Contest)
                .WithMany(c => c.Juries)
                .HasForeignKey(j => j.ContestId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
