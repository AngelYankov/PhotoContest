using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data.Configurations;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var roles = new List<Role>()
            {
                new Role() { Id = Guid.Parse("d0a458f4-cba3-4e49-a779-f79a9de41268"), Name = "Admin", NormalizedName = "ADMIN" },
                new Role() { Id = Guid.Parse("8a73d7c7-c092-4281-8cde-6dd9a9dd747c"), Name = "Organizer", NormalizedName = "ORGANIZER" },
                new Role() { Id = Guid.Parse("a117e076-855e-401a-aeab-844fee43a0a2"), Name = "User", NormalizedName = "USER" }
            };
            //seed users
            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("1d4c48e4-8870-417b-8ac6-e78efe1aaab5"),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@mail.com",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    UserName = "admin@mail.com",
                    NormalizedUserName = "ADMIN@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("a036e464-8996-4e40-9a81-39239cf72402"),
                    SecurityStamp = "DC6E275DD1E24957A7781D42BB68299B",
                    LockoutEnabled = true
                },
                new User()
                {
                    Id = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),
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
                },
                new User()
                {
                    Id = Guid.Parse("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"),
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
                },
                new User()
                {
                    Id = Guid.Parse("021fa300-ffd4-48e2-a93f-d40c17d014f3"),
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@mail.com",
                    NormalizedEmail = "JOHN.SMITH@MAIL.COM",
                    UserName = "john.smith@mail.com",
                    NormalizedUserName = "JOHN.SMITH@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),
                    SecurityStamp = "DC6E275DD1E25957A7781D42BB68299B",
                    LockoutEnabled = true
                },
                new User()
                {
                    Id = Guid.Parse("743f0e66-af28-48b9-8322-61395c10207f"),
                    FirstName = "Steven",
                    LastName = "King",
                    Email = "steven.king@mail.com",
                    NormalizedEmail = "STEVEN.KING@MAIL.COM",
                    UserName = "steven.king@mail.com",
                    NormalizedUserName = "STEVEN.KING@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),
                    SecurityStamp = "DC6E375DD1E25957A7781D42BB68299B",
                    LockoutEnabled = true
                },
                new User()
                {
                    Id = Guid.Parse("71cd9097-0c95-4af2-9e43-da7324880583"),
                    FirstName = "Robert",
                    LastName = "Scott",
                    Email = "robert.scott@mail.com",
                    NormalizedEmail = "ROBERT.SCOTT@MAIL.COM",
                    UserName = "robert.scott@mail.com",
                    NormalizedUserName = "ROBERT.SCOTT@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),
                    SecurityStamp = "DC6E375DD1E25957A7981D42BB68299B",
                    LockoutEnabled = true
                },
                new User()
                {
                    Id = Guid.Parse("7cc9804e-2106-4943-994d-91be3d1fab8e"),
                    FirstName = "Jimmy",
                    LastName = "Brown",
                    Email = "jimmy.brown@mail.com",
                    NormalizedEmail = "JIMMY.BROWN@MAIL.COM",
                    UserName = "jimmy.brown@mail.com",
                    NormalizedUserName = "JIMMY.BROWN@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),
                    SecurityStamp = "DC6E375DD1E25957A7981D42BB68399B",
                    LockoutEnabled = true
                },
                new User()
                {
                    Id = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb"),
                    FirstName = "Sam",
                    LastName = "Stevens",
                    Email = "sam.stevens@mail.com",
                    NormalizedEmail = "SAM.STEVENS@MAIL.COM",
                    UserName = "sam.stevens@mail.com",
                    NormalizedUserName = "SAM.STEVENS@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("a9576301-3157-454f-86ce-85bb5eb2dfc9"),
                    SecurityStamp = "DC6E375DD1E25957A7981D48BB68399B",
                    LockoutEnabled = true,
                    OverallPoints = 200
                },
                new User()
                {
                    Id = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76"),
                    FirstName = "Kyle",
                    LastName = "Sins",
                    Email = "kyle.sins@mail.com",
                    NormalizedEmail = "KYLE.SINS@MAIL.COM",
                    UserName = "kyle.sins@mail.com",
                    NormalizedUserName = "KYLE.SINS@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("0b1728c7-5582-4958-9e97-52c9b1d44cdb"),
                    SecurityStamp = "DC6E375DD2E25957A7981D48BB68399B",
                    LockoutEnabled = true,
                    OverallPoints = 1200
                },
                new User()
                {
                    Id = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"),
                    FirstName = "Sara",
                    LastName = "Smith",
                    Email = "sara.smith@mail.com",
                    NormalizedEmail = "SARA.SMITH@MAIL.COM",
                    UserName = "sara.smith@mail.com",
                    NormalizedUserName = "SARA.SMITH@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"),
                    SecurityStamp = "DC6E275DD1E24917A7781D42BB68293B",
                    LockoutEnabled = true
                },
                new User()
                {
                    Id = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),
                    FirstName = "Jane",
                    LastName = "Beck",
                    Email = "jane.beck@mail.com",
                    NormalizedEmail = "JANE.BECK@MAIL.COM",
                    UserName = "jane.beck@mail.com",
                    NormalizedUserName = "JANE.BECK@MAIL.COM",
                    CreatedOn = DateTime.UtcNow,
                    RankId = Guid.Parse("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"),
                    SecurityStamp = "DC6E275DD1E24917A7781D42BB64293B",
                    LockoutEnabled = true
                }
            };
            //hash passwords
            var passHasher = new PasswordHasher<User>();
            foreach (var user in users)
            {
                user.PasswordHash = passHasher.HashPassword(user, user.FirstName.ToLower() + "123");
            }
            //link userRoles
            var userRoles = new List<IdentityUserRole<Guid>>()
            {
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("d0a458f4-cba3-4e49-a779-f79a9de41268"),UserId = Guid.Parse("1d4c48e4-8870-417b-8ac6-e78efe1aaab5")}, //admin - admin
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("8a73d7c7-c092-4281-8cde-6dd9a9dd747c"),UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128")}, // eric berg - organizer
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("8a73d7c7-c092-4281-8cde-6dd9a9dd747c"),UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23")}, // Sara Smith - organizer
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("8a73d7c7-c092-4281-8cde-6dd9a9dd747c"),UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581")}, // Jane Beck - organizer
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("a117e076-855e-401a-aeab-844fee43a0a2"),UserId = Guid.Parse("8a20e519-66ad-46b8-b6c3-18c36fa50a1d")}, //georgi ivanov - junkie
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("a117e076-855e-401a-aeab-844fee43a0a2"),UserId = Guid.Parse("021fa300-ffd4-48e2-a93f-d40c17d014f3")}, //john smith - junkie
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("a117e076-855e-401a-aeab-844fee43a0a2"),UserId = Guid.Parse("743f0e66-af28-48b9-8322-61395c10207f")}, //steven king - junkie
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("a117e076-855e-401a-aeab-844fee43a0a2"),UserId = Guid.Parse("71cd9097-0c95-4af2-9e43-da7324880583")}, //robert scott - junkie
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("a117e076-855e-401a-aeab-844fee43a0a2"),UserId = Guid.Parse("7cc9804e-2106-4943-994d-91be3d1fab8e")}, //jimmy brown - junkie
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("a117e076-855e-401a-aeab-844fee43a0a2"),UserId = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb")}, //sam stevens - master-200points
                new IdentityUserRole<Guid>(){RoleId = Guid.Parse("a117e076-855e-401a-aeab-844fee43a0a2"),UserId = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76")} //kyle sins - dictator-1200points
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
                    Id = Guid.Parse("729b970a-ee54-4852-8ac7-d9b3146e886b"),
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
                    Id = Guid.Parse("af4ea8a0-8e69-4746-bbc8-aa4593a11828"),
                    Name = "Architecture",
                    CreatedOn = DateTime.UtcNow,
                },
                new Category()
                {
                    Id = Guid.Parse("fad09db4-8187-4777-9e68-3ba40218c7d3"),
                    Name = "Motorcycles",
                    CreatedOn = DateTime.UtcNow,
                }
            };
            //seed statuses
            var statuses = new List<Status>()
            {
                new Status()
                {
                    Id = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"),
                    Name = "Phase 1"
                },
                new Status()
                {
                    Id = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc"),
                    Name = "Phase 2"
                },
                new Status()
                {
                    Id = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"),
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
                    Id = Guid.Parse("a9576301-3157-454f-86ce-85bb5eb2dfc9"),
                    Name = "Master"
                },
                new Rank()
                {
                    Id = Guid.Parse("0b1728c7-5582-4958-9e97-52c9b1d44cdb"),
                    Name = "Wise and Benevolent Photo Dictator"
                },
                new Rank()
                {
                    Id = Guid.Parse("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"),
                    Name = "Organizer"
                },
                new Rank()
                {
                    Id = Guid.Parse("a036e464-8996-4e40-9a81-39239cf72402"),
                    Name = "Admin"
                }
            };
            //seed contents
            var contests = new List<Contest>()
            {
                new Contest()
                {
                    Id = Guid.Parse("f36e97ee-98af-4f26-93ef-066895d94b2a"),
                    Name = "Wild cats",
                    CategoryId = Guid.Parse("729b970a-ee54-4852-8ac7-d9b3146e886b"),
                    StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"),
                    IsOpen = true,
                    Phase1 = DateTime.UtcNow,
                    Phase2 = DateTime.Parse("10-06-2021 09:00", CultureInfo.InvariantCulture),
                    Finished = DateTime.Parse("10-06-2021 19:00",CultureInfo.InvariantCulture)
                },
                new Contest()
                {
                    Id = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    Name = "Best look",
                    CategoryId = Guid.Parse("fad09db4-8187-4777-9e68-3ba40218c7d3"),
                    StatusId = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc"),
                    IsOpen = true,
                    Phase1 = DateTime.Parse("15-05-2021 09:00", CultureInfo.InvariantCulture),
                    Phase2 = DateTime.Parse("25-05-2021 12:00", CultureInfo.InvariantCulture),
                    Finished = DateTime.Parse("26-05-2021 09:00",CultureInfo.InvariantCulture)
                },
                new Contest()
                {
                    Id = Guid.Parse("42541f52-8d30-4828-bf66-4eda82735edd"),
                    Name = "Best building",
                    CategoryId = Guid.Parse("af4ea8a0-8e69-4746-bbc8-aa4593a11828"),
                    StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"),
                    IsOpen = true,
                    Phase1 = DateTime.Parse("10-05-2021 09:00", CultureInfo.InvariantCulture),
                    Phase2 = DateTime.Parse("20-05-2021 12:00", CultureInfo.InvariantCulture),
                    Finished = DateTime.Parse("20-05-2021 09:00",CultureInfo.InvariantCulture)
                },
                new Contest()
                {
                    Id = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    Name = "Birds",
                    CategoryId = Guid.Parse("729b970a-ee54-4852-8ac7-d9b3146e886b"),
                    StatusId = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc"),
                    IsOpen = true,
                    Phase1 = DateTime.Parse("10-05-2021 09:00", CultureInfo.InvariantCulture),
                    Phase2 = DateTime.UtcNow,
                    Finished = DateTime.Parse("26-05-2021 09:00",CultureInfo.InvariantCulture)
                }
            };
            //ssed photos
            var photos = new List<Photo>()
            {
                new Photo()
                {

                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Status>().HasData(statuses);
            modelBuilder.Entity<Rank>().HasData(ranks);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<Contest>().HasData(contests);
            modelBuilder.Entity<Photo>().HasData(photos);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRoles);
        }
    }
}

