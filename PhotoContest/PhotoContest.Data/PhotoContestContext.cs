using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data.Configurations;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoContest.Data
{
    public class PhotoContestContext : IdentityDbContext<User, Role, Guid>
    {
        public PhotoContestContext() { }
        public PhotoContestContext(DbContextOptions<PhotoContestContext> options)
            : base(options) { }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserContest> UserContests { get; set; }
        public DbSet<JuryMember> Juries { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new ContestConfig());
            modelBuilder.ApplyConfiguration(new JuryMemberConfig());
            modelBuilder.ApplyConfiguration(new PhotoConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserContestConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());

            this.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options.IsConfigured)
                options.UseSqlServer("Server=.\\SQLEXPRESS; Database=PhotoContestDB; Integrated Security=True");
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
            //seed roles 
            var adminRole = new Role() { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" };
            var organizerRole = new Role() { Id = Guid.NewGuid(), Name = "Organizer", NormalizedName = "ORGANIZER" };
            var userRole = new Role() { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" };

            //password hasher
            var passHasher = new PasswordHasher<User>();

            //seed organizer
            var organizer = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Eric",
                LastName = "Berg",
                Email = "eric.berg@mail.com",
                NormalizedEmail = "ERIC.BERG@MAIL.COM",
                UserName = "eric.berg@mail.com",
                NormalizedUserName = "ERIC.BERG@MAIL.COM",
                CreatedOn = DateTime.UtcNow,
                RankId = Guid.Parse("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"),
                SecurityStamp = "DC6E275DD1E24957A7781D42BB68293B",
                LockoutEnabled = true
            };
            organizer.PasswordHash = passHasher.HashPassword(organizer, "eric.berg123");

            //seed user
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Georgi",
                LastName = "Ivanov",
                Email = "georgi.ivanov@mail.com",
                NormalizedEmail = "GEORGI.IVANOV@MAIL.COM",
                UserName = "georgi.ivanov@mail.com",
                NormalizedUserName = "GEORGI.IVANOV@MAIL.COM",
                CreatedOn = DateTime.UtcNow,
                RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),
                SecurityStamp = "DC6E275DD1E24957A7781D42BB68292B",
                LockoutEnabled = true
            };
            user.PasswordHash = passHasher.HashPassword(user, "georgi.ivanov123");

            //seed admin
            var admin = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                CreatedOn = DateTime.UtcNow,
                //RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),
                SecurityStamp = "DC6E275DD1E24957A7781D42BB68299B",
                LockoutEnabled = true
            };
            admin.PasswordHash = passHasher.HashPassword(admin, "admin123");

            //link user to role
            var normalUserRole = new IdentityUserRole<Guid>()
            {
                RoleId = userRole.Id,
                UserId = user.Id
            };
            //link organizer to role
            var organizerUserRole = new IdentityUserRole<Guid>()
            {
                RoleId = organizerRole.Id,
                UserId = organizer.Id
            };
            //link admin to role
            var adminUserRole = new IdentityUserRole<Guid>()
            {
                RoleId = adminRole.Id,
                UserId = admin.Id
            };

            //seed categories
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Cars",
                    CreatedOn = DateTime.UtcNow,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Animals",
                    CreatedOn = DateTime.UtcNow,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nature",
                    CreatedOn = DateTime.UtcNow,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Architecture",
                    CreatedOn = DateTime.UtcNow,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Motorbikes",
                    CreatedOn = DateTime.UtcNow,
                }
            };
            //seed statuses
            var statuses = new List<Status>()
            {
                new Status()
                {
                    Id = Guid.NewGuid(),
                    Name = "Phase 1"
                },
                new Status()
                {
                    Id = Guid.NewGuid(),
                    Name = "Phase 2"
                },
                new Status()
                {
                    Id = Guid.NewGuid(),
                    Name = "Finished"
                },
            };
            //seed ranks
            var ranks = new List<Rank>()
            {
                new Rank()
                {
                    Id = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),
                    Name = "Junkie"
                },
                new Rank()
                {
                    Id = Guid.NewGuid(),
                    Name = "Enthusiast"
                },
                new Rank()
                {
                    Id = Guid.NewGuid(),
                    Name = "Master"
                },
                new Rank()
                {
                    Id = Guid.NewGuid(),
                    Name = "Wise and Benevolent Photo Dictator"
                },
                new Rank()
                {
                    Id = Guid.Parse("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"),
                    Name = "Organizer"
                }
            };
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Status>().HasData(statuses);
            modelBuilder.Entity<Rank>().HasData(ranks);
            modelBuilder.Entity<User>().HasData(admin, user, organizer);
            modelBuilder.Entity<Role>().HasData(adminRole,organizerRole, userRole);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(adminUserRole,normalUserRole, organizerUserRole);
        }
    }
}
