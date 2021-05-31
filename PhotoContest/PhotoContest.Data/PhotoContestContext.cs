using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data.Configurations;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PhotoContest.Data
{
    public class PhotoContestContext : IdentityDbContext<User, Role, Guid>
    {
        //private readonly IWebHostEnvironment webHost;

        public PhotoContestContext() { }
        public PhotoContestContext(DbContextOptions<PhotoContestContext> options)
            : base(options)
        {
            //this.webHost = webHost;
        }
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

        /*protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options.IsConfigured)
                options.UseSqlServer("Server=.\\SQLEXPRESS; Database=PhotoContestDB; Integrated Security=True");
        }*/

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
                    Id = Guid.Parse("41c8e397-f768-48ed-b8f1-f8a238c739b1"),
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
                    StatusId = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc"),
                    IsOpen = true,
                    Phase1 = new DateTime(2021,05,10,9,0,0),
                    Phase2 = DateTime.UtcNow,
                    Finished = DateTime.UtcNow.AddMinutes(2)
                },
                new Contest()
                {
                    Id = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    Name = "Best look",
                    CategoryId = Guid.Parse("fad09db4-8187-4777-9e68-3ba40218c7d3"),
                    StatusId = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc"),
                    IsOpen = true,
                    Phase1 = new DateTime(2021,05,15,9,0,0),
                    Phase2 = new DateTime(2021,05,25,12,0,0),
                    Finished =new DateTime(2021,05,26,9,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("42541f52-8d30-4828-bf66-4eda82735edd"),
                    Name = "Best building",
                    CategoryId = Guid.Parse("af4ea8a0-8e69-4746-bbc8-aa4593a11828"),
                    StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"),
                    IsOpen = true,
                    Phase1 =  new DateTime(2021,05,10,9,0,0),
                    Phase2 = new DateTime(2021,05,20,12,0,0),// DateTime.ParseExact("20-05-2021 12:00","dd-MM-yy HH:mm", CultureInfo.InvariantCulture),
                    Finished = new DateTime(2021,05,20,9,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    Name = "Birds",
                    CategoryId = Guid.Parse("729b970a-ee54-4852-8ac7-d9b3146e886b"),
                    StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"),
                    IsOpen = true,
                    Phase1 = DateTime.UtcNow,
                    Phase2 = new DateTime(2021,06,10,9,0,0),
                    Finished = new DateTime(2021,06,10,19,0,0)
                }
            };
            //seed photos
            var photos = new List<Photo>()
            {
                new Photo()
                {
                    Id = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"),
                    ContestId = Guid.Parse("f36e97ee-98af-4f26-93ef-066895d94b2a"),
                    UserId = Guid.Parse("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"),
                    Title = "Lion King",
                    Description = "Picture of a lion.",
                    PhotoUrl = "/Images/lion.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"),
                    ContestId = Guid.Parse("f36e97ee-98af-4f26-93ef-066895d94b2a"),
                    UserId = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb"),
                    Title = "Tiger",
                    Description = "Picture of a tiger.",
                    PhotoUrl = "/Images/tiger.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"),
                    ContestId = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    UserId = Guid.Parse("021fa300-ffd4-48e2-a93f-d40c17d014f3"),
                    Title = "Kawasaki Ninja",
                    Description = "Picture of a Kawasaki.",
                    PhotoUrl = "/Images/kawasaki.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                 new Photo()
                {
                    Id = Guid.Parse("507c5f65-497b-4a3c-95f6-cfbc86692ca5"),
                    ContestId = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    UserId = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76"),
                    Title = "Honda CBR",
                    Description = "Picture of a Honda.",
                    PhotoUrl = "/Images/honda.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                 new Photo()
                {
                    Id = Guid.Parse("59dd9540-a1d8-4360-99d5-ed8302aae5e2"),
                    ContestId = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    UserId = Guid.Parse("7cc9804e-2106-4943-994d-91be3d1fab8e"),
                    Title = "Collibri",
                    Description = "Picture of a colibri.",
                    PhotoUrl = "/Images/colibri.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                 new Photo()
                {
                    Id = Guid.Parse("94499cdd-e18c-4743-b0c4-2e1b7564c46c"),
                    ContestId = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    UserId = Guid.Parse("71cd9097-0c95-4af2-9e43-da7324880583"),
                    Title = "Eagle",
                    Description = "Picture of an eagle.",
                    PhotoUrl = "/Images/eagle.jpg",
                    CreatedOn = DateTime.UtcNow
                }
            };
            //seed reviews
            var reviews = new List<Review>()
            {
                new Review()
                {
                    Id = Guid.Parse("f55244de-da0f-4a9c-b8d9-7940a2f97083"),
                    Comment = "Not so great quality of the picture.",
                    Score = 4,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),
                    PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("8198e13a-30cb-4f4b-99f0-acf31a70b02d"),
                    Comment = "Great lion.",
                    Score = 8,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),
                    PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("73fe1a7a-e31c-4b4e-a6fa-1ae65e7e1f28"),
                    Comment = "Marvelous tiger.",
                    Score = 10,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),
                    PhotoId = Guid.Parse("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("b8e942fb-9e23-48b2-b15f-32a1e2c06315"),
                    Comment = "Skinny tiger.",
                    Score = 3,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"),
                    PhotoId = Guid.Parse("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("55cf8205-bfb9-4d8c-8ac1-7861a458bb10"),
                    Comment = "Very good colour.",
                    Score = 9,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),
                    PhotoId = Guid.Parse("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("546ff836-f1c5-46e2-ba55-50729daf0419"),
                    Comment = "Not a very good setting.",
                    Score = 6,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"),
                    PhotoId = Guid.Parse("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                }
            };
            var userContests = new List<UserContest>()
            {
                new UserContest()
                {
                    Id = Guid.Parse("be9c8856-5df8-4577-a7c9-f8f62f8de22c"),
                    ContestId = Guid.Parse("f36e97ee-98af-4f26-93ef-066895d94b2a"),
                    UserId = Guid.Parse("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"),
                },
                new UserContest()
                {
                    Id = Guid.Parse("61f45846-09fd-4112-b3b8-5aaf029e8a9f"),
                    ContestId = Guid.Parse("f36e97ee-98af-4f26-93ef-066895d94b2a"),
                    UserId = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb"),
                },
                new UserContest()
                {
                    Id = Guid.Parse("d00fb4ba-c05c-4a48-8042-0db3b747b226"),
                    ContestId = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    UserId = Guid.Parse("021fa300-ffd4-48e2-a93f-d40c17d014f3"),
                },
                new UserContest()
                {
                    Id = Guid.Parse("f933eff8-9a79-4937-801a-a80aaa8d4b19"),
                    ContestId = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    UserId = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76"),
                },
                new UserContest()
                {
                    Id = Guid.Parse("1e1008e0-63f6-437a-8c86-347dcf905b7d"),
                    ContestId = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    UserId = Guid.Parse("7cc9804e-2106-4943-994d-91be3d1fab8e"),
                },
                new UserContest()
                {
                    Id = Guid.Parse("bb047135-03e9-4957-8248-306eaf8600cc"),
                    ContestId = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    UserId = Guid.Parse("71cd9097-0c95-4af2-9e43-da7324880583"),
                }
            };
            

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Status>().HasData(statuses);
            modelBuilder.Entity<Rank>().HasData(ranks);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<Contest>().HasData(contests);
            modelBuilder.Entity<Photo>().HasData(photos);
            modelBuilder.Entity<Review>().HasData(reviews);
            modelBuilder.Entity<UserContest>().HasData(userContests);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRoles);
        }
    }
}

