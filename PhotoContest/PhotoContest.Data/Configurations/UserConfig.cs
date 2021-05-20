using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhotoContest.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();

            /*builder.HasOne(u => u.Rank)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RankId)
                .OnDelete(DeleteBehavior.NoAction);*/
        }
    }
}
