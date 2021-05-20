using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Configurations
{
    public class ContestConfig : IEntityTypeConfiguration<Contest>
    {
        public void Configure(EntityTypeBuilder<Contest> builder)
        {
            builder.HasOne(contest => contest.Category)
                .WithMany(category => category.Contests)
                .HasForeignKey(contest => contest.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(contest => contest.Status)
                .WithMany(status => status.Contests)
                .HasForeignKey(contest => contest.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
