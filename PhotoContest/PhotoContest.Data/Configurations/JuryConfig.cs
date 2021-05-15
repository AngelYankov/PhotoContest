using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Data.Models;

namespace PhotoContest.Data.Configurations
{
    public class JuryConfig : IEntityTypeConfiguration<Jury>
    {
        public void Configure(EntityTypeBuilder<Jury> builder)
        {
            builder.HasKey(j => new { j.UserId, j.ContestId });

            builder.HasOne(j => j.User)
                .WithMany(u => u.Juries)
                .HasForeignKey(j => j.UserId);
            
            builder.HasOne(j => j.Contest)
                .WithMany(c => c.Juries)
                .HasForeignKey(j => j.ContestId);
        }
    }
}
