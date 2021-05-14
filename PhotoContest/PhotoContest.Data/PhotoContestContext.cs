using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;

namespace PhotoContest.Data
{
    public class PhotoContestContext : IdentityDbContext<User, Role, Guid>
    {
        public PhotoContestContext()
        {

        }
        public PhotoContestContext(DbContextOptions<PhotoContestContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
                
            var adminRole = new Role() { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" };
            var organizerRole = new Role() { Id = Guid.NewGuid(), Name = "Organizer", NormalizedName = "ORGANIZER" };
            var userRole = new Role() { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" };
            var roles = new List<Role>() { adminRole, organizerRole, userRole };

            modelBuilder.Entity<Role>().HasData(roles); 

            //password hasher
            var passHasher = new PasswordHasher<User>();

            //seed admin user
            var adminUser = new User();
            adminUser.Id = Guid.NewGuid();
            adminUser.Email = "admin@admin.com";
            adminUser.NormalizedEmail = "ADMIN@ADMIN.COM";
            adminUser.PasswordHash = passHasher.HashPassword(adminUser, "admin123");
            adminUser.SecurityStamp = Guid.NewGuid().ToString();
            modelBuilder.Entity<User>().HasData(adminUser);

            //link adminUser to role
            var adminUserRole = new IdentityUserRole<Guid>();
            adminUserRole.RoleId = adminRole.Id;
            adminUserRole.UserId = adminUser.Id;
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(adminUserRole);

            //seed organizerUser
            var organizerUser = new Organizer();
            organizerRole.Id = Guid.NewGuid();
            organizerUser.FirstName = "Eric";
            organizerUser.LastName = "Berg";
            organizerUser.Email = "eric.berg@mail.com";
            organizerUser.NormalizedEmail = "ERIC.BERG@MAIL.COM";
            organizerUser.UserName = "eric.berg@mail.com";
            organizerUser.NormalizedUserName = "ERIC.BERG@MAIL.COM";
            organizerUser.CreatedOn = DateTime.UtcNow;
            organizerUser.PasswordHash = passHasher.HashPassword(organizerUser, "eric.berg123");
            organizerUser.SecurityStamp = Guid.NewGuid().ToString();
            modelBuilder.Entity<User>().HasData(organizerUser);

            //link organizerUser to role
            var organizerUserRole = new IdentityUserRole<Guid>();
            organizerUserRole.RoleId = organizerRole.Id;
            organizerUserRole.UserId = organizerUser.Id;
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(organizerUserRole);

            //seed userRole
            var fanUser = new Fan();
            fanUser.Id = Guid.NewGuid();
            fanUser.FirstName = "Georgi";
            fanUser.LastName = "Ivanov";
            fanUser.Email = "georgi.ivanov@mail.com";
            fanUser.NormalizedEmail = "GEORGI.IVANOV@MAIL.COM";
            fanUser.UserName = "georgi.ivanov@mail.com";
            fanUser.NormalizedUserName = "GEORGI.IVANOV@MAIL.COM";
            fanUser.CreatedOn = DateTime.UtcNow;
            fanUser.PasswordHash = passHasher.HashPassword(fanUser, "georgi.ivanov123");
            fanUser.SecurityStamp = Guid.NewGuid().ToString();
            modelBuilder.Entity<User>().HasData(fanUser);

            //link fanUser to role
            var fanUserRole = new IdentityUserRole<Guid>();
            fanUserRole.RoleId = userRole.Id;
            fanUserRole.UserId = fanUser.Id;
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(fanUserRole);

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
            };
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
            var ranks = new List<Rank>()
            {
                new Rank()
                {
                    Id = Guid.NewGuid(),
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
                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Status>().HasData(statuses);
            modelBuilder.Entity<Rank>().HasData(ranks);
        }
    }
}
