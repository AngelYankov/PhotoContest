﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Configurations
{
    public class UserContestConfig : IEntityTypeConfiguration<UserContest>
    {
        public void Configure(EntityTypeBuilder<UserContest> builder)
        {
            builder.HasKey(u => new { u.ContestId, u.UserId });

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserContests)
                .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.Contest)
                .WithMany(c => c.UserContests)
                .HasForeignKey(uc => uc.ContestId);
        }
    }
}
