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
        public PhotoContestContext() { }
        public PhotoContestContext(DbContextOptions<PhotoContestContext> options)
            : base(options)
        {
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
                    RankId = Guid.Parse("a036e464-8996-4e40-9a81-39239cf72402"),//admin
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
                    RankId = Guid.Parse("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"),//organizer
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
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),//junkie
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
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),//junkie
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
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),//junkie
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
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),//junkie
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
                    RankId = Guid.Parse("acca215b-d737-406c-b87c-696fb22ce001"),//junkie
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
                    RankId = Guid.Parse("a9576301-3157-454f-86ce-85bb5eb2dfc9"),//master
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
                    RankId = Guid.Parse("0b1728c7-5582-4958-9e97-52c9b1d44cdb"),//wise and benevolent
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
                    RankId = Guid.Parse("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"),//organizer
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
                    RankId = Guid.Parse("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), //organizer
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
                    Id = Guid.Parse("e43364e1-28d4-48c3-b4e2-5bd5f43b89b0"),
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
                    Id = Guid.Parse("ad729c24-eda2-4209-93c8-f80d1f47172c"),
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
                },
                new Category()
                {
                    Id = Guid.Parse("a8c71a51-79f2-46ef-8a88-7983cbb7259a"),
                    Name = "People",
                    CreatedOn = DateTime.UtcNow,
                },
                new Category()
                {
                    Id = Guid.Parse("28f87c5a-b02a-4422-8b71-4821306279d5"),
                    Name = "Sports",
                    CreatedOn = DateTime.UtcNow,
                },

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
                    Id = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    Name = "Birds",
                    CategoryId = Guid.Parse("729b970a-ee54-4852-8ac7-d9b3146e886b"), //animals
                    StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"),//phase1
                    IsOpen = true,
                    Phase1 = DateTime.UtcNow,
                    Phase2 = new DateTime(2021,06,10,9,0,0),
                    Finished = new DateTime(2021,06,10,19,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("f8d22e50-664b-483f-a542-ab26135e6772"),
                    Name = "Skyscrapers",
                    CategoryId = Guid.Parse("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), //architecture
                    StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"),//phase1
                    IsOpen = true,
                    Phase1 = DateTime.UtcNow,
                    Phase2 = new DateTime(2021,06,14,9,0,0),
                    Finished = new DateTime(2021,06,14,19,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("fd4ec4c7-3976-4a44-9dfa-06967ab471c1"),
                    Name = "Snakes",
                    CategoryId = Guid.Parse("729b970a-ee54-4852-8ac7-d9b3146e886b"), //animals
                    StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"),//phase1
                    IsOpen = true,
                    Phase1 = DateTime.UtcNow,
                    Phase2 = new DateTime(2021,06,15,9,0,0),
                    Finished = new DateTime(2021,06,15,19,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("b2ba5698-bc3a-45ee-8dcb-06e6a2e99a09"),
                    Name = "Muscle cars",
                    CategoryId = Guid.Parse("e43364e1-28d4-48c3-b4e2-5bd5f43b89b0"), //cars
                    StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"),//phase1
                    IsOpen = true,
                    Phase1 = DateTime.UtcNow,
                    Phase2 = new DateTime(2021,06,16,9,0,0),
                    Finished = new DateTime(2021,06,16,19,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("c5d76f38-f3f9-4aaa-931f-0d91e0207a0a"),
                    Name = "Lakes",
                    CategoryId = Guid.Parse("ad729c24-eda2-4209-93c8-f80d1f47172c"), //nature
                    StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"),//phase1
                    IsOpen = true,
                    Phase1 = DateTime.UtcNow,
                    Phase2 = new DateTime(2021,06,17,9,0,0),
                    Finished = new DateTime(2021,06,17,19,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("17578ed3-fdfc-4616-9cdb-55a5ff762caf"),
                    Name = "Couples",
                    CategoryId = Guid.Parse("a8c71a51-79f2-46ef-8a88-7983cbb7259a"), //people
                    StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"), //phase1
                    IsOpen = true,
                    Phase1 = DateTime.UtcNow,
                    Phase2 = new DateTime(2021,06,18,9,0,0),
                    Finished = new DateTime(2021,06,18,19,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("f36e97ee-98af-4f26-93ef-066895d94b2a"),
                    Name = "Wild cats",
                    CategoryId = Guid.Parse("729b970a-ee54-4852-8ac7-d9b3146e886b"), //animals
                    StatusId = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc"), //phase 2
                    IsOpen = true,
                    Phase1 = new DateTime(2021,05,10,9,0,0),
                    Phase2 = DateTime.UtcNow,
                    Finished = DateTime.UtcNow.AddHours(24)
                },
                new Contest()
                {
                    Id = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    Name = "Best look",
                    CategoryId = Guid.Parse("fad09db4-8187-4777-9e68-3ba40218c7d3"), //motorcycles
                    StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"),  //finished
                    IsOpen = true,
                    Phase1 = new DateTime(2021,05,15,9,0,0),
                    Phase2 = new DateTime(2021,05,25,12,0,0),
                    Finished =new DateTime(2021,05,26,9,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("42541f52-8d30-4828-bf66-4eda82735edd"),
                    Name = "Mansions",
                    CategoryId = Guid.Parse("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), //architecture
                    StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"), //finished
                    IsOpen = true,
                    Phase1 =  new DateTime(2021,05,10,9,0,0),
                    Phase2 = new DateTime(2021,05,20,12,0,0),
                    Finished = new DateTime(2021,05,20,9,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("f9f89a56-448a-43c4-a098-fe5b13605999"),
                    Name = "Bridges",
                    CategoryId = Guid.Parse("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), //architecture
                    StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"), //finished
                    IsOpen = true,
                    Phase1 =  new DateTime(2021,05,10,9,0,0),
                    Phase2 = new DateTime(2021,05,20,12,0,0),
                    Finished = new DateTime(2021,05,20,9,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("d7b46312-7197-4c79-8384-1ec2b8577f8d"),
                    Name = "Historic landmarks",
                    CategoryId = Guid.Parse("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), //architecture
                    StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"), //finished
                    IsOpen = true,
                    Phase1 =  new DateTime(2021,05,10,9,0,0),
                    Phase2 = new DateTime(2021,05,20,12,0,0),
                    Finished = new DateTime(2021,05,20,9,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("d2acdd9c-9427-4fc2-897e-5f52da2190dc"),
                    Name = "Olympics",
                    CategoryId = Guid.Parse("28f87c5a-b02a-4422-8b71-4821306279d5"), //sports
                    StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"), //finished
                    IsOpen = true,
                    Phase1 =  new DateTime(2021,05,10,9,0,0),
                    Phase2 = new DateTime(2021,05,20,12,0,0),
                    Finished = new DateTime(2021,05,20,9,0,0)
                },
                new Contest()
                {
                    Id = Guid.Parse("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"),
                    Name = "Sunsets",
                    CategoryId = Guid.Parse("ad729c24-eda2-4209-93c8-f80d1f47172c"), //nature
                    StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"), //finished
                    IsOpen = true,
                    Phase1 =  new DateTime(2021,05,10,9,0,0),
                    Phase2 = new DateTime(2021,05,20,12,0,0),
                    Finished = new DateTime(2021,05,20,9,0,0)
                },

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
                    Description = "Roaring of a lion.",
                    PhotoUrl = $"/Images/c4baabc4-bd02-4bd6-bb33-955556530c8e_lion1.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"),
                    ContestId = Guid.Parse("f36e97ee-98af-4f26-93ef-066895d94b2a"),
                    UserId = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb"),
                    Title = "Tiger",
                    Description = "Going for the kill.",
                    PhotoUrl = $"/Images/da80d3a0-2aaa-4360-871e-699b5507277f_tiger1.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"),
                    ContestId = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    UserId = Guid.Parse("021fa300-ffd4-48e2-a93f-d40c17d014f3"),
                    Title = "Kawasaki",
                    Description = "Parked for the day.",
                    PhotoUrl = $"/Images/16bb1fa0-8f61-4717-bba1-e14f8c47b616_kawasaki1.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("507c5f65-497b-4a3c-95f6-cfbc86692ca5"),
                    ContestId = Guid.Parse("548873db-705b-46e7-b88d-230c5f06fd35"),
                    UserId = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76"),
                    Title = "Honda CBR",
                    Description = "Takaing a small break from the road.",
                    PhotoUrl = $"/Images/dcc53eb5-2024-4242-877c-423e9c0d751f_honda1.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("59dd9540-a1d8-4360-99d5-ed8302aae5e2"),
                    ContestId = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    UserId = Guid.Parse("7cc9804e-2106-4943-994d-91be3d1fab8e"),
                    Title = "Hummingbird",
                    Description = "Hummingbird on a branch.",
                    PhotoUrl = $"/Images/3a425bea-85d7-4473-850b-afb9162dfe7e_colibri1.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("94499cdd-e18c-4743-b0c4-2e1b7564c46c"),
                    ContestId = Guid.Parse("e2450bf8-c019-4442-a2c3-ed0210586eed"),
                    UserId = Guid.Parse("71cd9097-0c95-4af2-9e43-da7324880583"),
                    Title = "Eagle",
                    Description = "Scanning for prey.",
                    PhotoUrl = $"/Images/1d2dfd54-7d9b-41c4-b442-995baa1ac289_eagle1.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                //new
                new Photo()
                {
                    Id = Guid.Parse("5edcd3ee-3033-43fe-a222-97fdac5922ab"),
                    ContestId = Guid.Parse("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"),//sunsets
                    UserId = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76"), //kyle
                    Title = "African sunset",
                    Description = "Beautiful sunset in Kenya",
                    PhotoUrl = $"/Images/ccf1a5e0-b616-4e99-a524-6691e79ca16b_sunset1.jpg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("44e5ce67-461c-4082-9acb-c5aceae13a0c"),
                    ContestId = Guid.Parse("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"),//sunsets
                    UserId = Guid.Parse("7cc9804e-2106-4943-994d-91be3d1fab8e"), //jimmy
                    Title = "New York sunset",
                    Description = "Sunset over the Statue of Liberty",
                    PhotoUrl = $"/Images/7ef91ce8-58f5-416d-a8c2-56d0218e6ebf_sunset2.jpg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("7780497c-f5e4-43e2-8e90-5067f91475ce"),
                    ContestId = Guid.Parse("d2acdd9c-9427-4fc2-897e-5f52da2190dc"), //olympics
                    UserId = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb"), //sam
                    Title = "Usain Bolt",
                    Description = "Usain Bolt breaking the record",
                    PhotoUrl = $"/Images/cb2acc2c-2576-4707-86ae-754e983e1f55_olympics1.jpg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("05505a4d-f749-46e9-928c-039dad92c808"),
                    ContestId = Guid.Parse("d2acdd9c-9427-4fc2-897e-5f52da2190dc"), //olympics
                    UserId = Guid.Parse("71cd9097-0c95-4af2-9e43-da7324880583"), // robert
                    Title = "Michael Phelps",
                    Description = "Michael Phelps winning gold",
                    PhotoUrl = $"/Images/71a3e79d-ee0e-4d4b-9a03-2890c42be96a_olympics2.jpg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("8a178e81-d2c3-44bb-a3d7-79d84f5188b2"),
                    ContestId = Guid.Parse("d7b46312-7197-4c79-8384-1ec2b8577f8d"), //historic landmarks
                    UserId = Guid.Parse("743f0e66-af28-48b9-8322-61395c10207f"), //steven
                    Title = "The Colloseum",
                    Description = "The mighty arena of gladiators",
                    PhotoUrl = $"/Images/a6e65cfd-81dd-4c64-aa05-11946f97b102_landmarks1.jpg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("351b9dbe-4142-4283-bbc4-f90a8a503925"),
                    ContestId = Guid.Parse("d7b46312-7197-4c79-8384-1ec2b8577f8d"), //historic landmarks
                    UserId = Guid.Parse("021fa300-ffd4-48e2-a93f-d40c17d014f3"), //john
                    Title = "The Pyramids",
                    Description = "The last standing wonder of the old world",
                    PhotoUrl = $"/Images/a562ac79-7f5e-48e7-a364-43cc656227bb_landmarks2.jpg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("08f2b9a9-37a8-4852-86c1-4058a62848a9"),
                    ContestId = Guid.Parse("f9f89a56-448a-43c4-a098-fe5b13605999"), //bridges
                    UserId = Guid.Parse("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"), //georgi
                    Title = "Erasmusbrug",
                    Description = "Man made beauty",
                    PhotoUrl = $"/Images/b364f4aa-a6af-4570-966f-f1980c6eb636_bridges1.jpeg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("2b7fb35a-9840-4d4b-b86e-a5e3040117e4"),
                    ContestId = Guid.Parse("f9f89a56-448a-43c4-a098-fe5b13605999"), //bridges
                    UserId = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76"), //kyle
                    Title = "Goldengate",
                    Description = "Engineering genius",
                    PhotoUrl = $"/Images/52dc2b1e-112c-4914-98b9-13137abd989a_bridges2.jpeg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("723b8f6d-95de-487f-a4b2-52f8be99ce11"),
                    ContestId = Guid.Parse("42541f52-8d30-4828-bf66-4eda82735edd"), //mansions
                    UserId = Guid.Parse("743f0e66-af28-48b9-8322-61395c10207f"), //steven
                    Title = "Palm house",
                    Description = "Great looking family house",
                    PhotoUrl = $"/Images/0eaa5bc4-a7b1-41e2-8fe9-543cf9fa2e46_mansions.jpg",//get picture
                    CreatedOn = DateTime.UtcNow
                },
                new Photo()
                {
                    Id = Guid.Parse("94da69ad-71c2-4dca-87a9-def9154ec7b0"),
                    ContestId = Guid.Parse("42541f52-8d30-4828-bf66-4eda82735edd"), //mansions
                    UserId = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb"), //sam
                    Title = "Mountain villa",
                    Description = "Nothing like fresh mountain air",
                    PhotoUrl = $"/Images/c77ff246-1712-4ab3-84cf-15b25966aebd_mansions2.jpg",//get picture
                    CreatedOn = DateTime.UtcNow
                },

            };
            //seed reviews
            var reviews = new List<Review>()
            {
                new Review()
                {
                    Id = Guid.Parse("f55244de-da0f-4a9c-b8d9-7940a2f97083"),
                    Comment = "Great angle.",
                    Score = 6,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),
                    PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("8198e13a-30cb-4f4b-99f0-acf31a70b02d"),
                    Comment = "Perfect timing.",
                    Score = 8,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),
                    PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("73fe1a7a-e31c-4b4e-a6fa-1ae65e7e1f28"),
                    Comment = "Perfect shot.",
                    Score = 10,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),
                    PhotoId = Guid.Parse("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("b8e942fb-9e23-48b2-b15f-32a1e2c06315"),
                    Comment = "Shadows are in the way.",
                    Score = 3,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"),
                    PhotoId = Guid.Parse("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("55cf8205-bfb9-4d8c-8ac1-7861a458bb10"),
                    Comment = "Great shot.",
                    Score = 9,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),
                    PhotoId = Guid.Parse("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"),
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                //new
                new Review()    
                {
                    Id = Guid.Parse("18d39ed7-120f-4239-8920-b0b826dd3d0a"),//1
                    Comment = "Mesmerizing shot.",
                    Score = 10,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), //sara
                    PhotoId = Guid.Parse("5edcd3ee-3033-43fe-a222-97fdac5922ab"),//african sunset
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("a15576fd-9e09-4b2a-b961-60b9204e803b"),//1
                    Comment = "Unique colors.",
                    Score = 8,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),//eric
                    PhotoId = Guid.Parse("5edcd3ee-3033-43fe-a222-97fdac5922ab"),//african sunset
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("5405c1fb-ab2e-4ada-95f2-c37436d3e24c"),//2
                    Comment = "Not really shot of a sunset.",
                    Score = 5,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),//jane
                    PhotoId = Guid.Parse("44e5ce67-461c-4082-9acb-c5aceae13a0c"),//new york sunset
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("7e2c2b98-9616-479a-bd1f-61d0510ccb15"),//2
                    Comment = "Great perspective.",
                    Score = 8,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),//eric
                    PhotoId = Guid.Parse("44e5ce67-461c-4082-9acb-c5aceae13a0c"),//new york sunset
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("98deadcb-ffb0-47c7-ac06-4dd5c8fc22fa"),//3
                    Comment = "Great quality.",
                    Score = 8,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),//jane
                    PhotoId = Guid.Parse("7780497c-f5e4-43e2-8e90-5067f91475ce"),//the best
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("d9beb14d-56c8-42d1-a6b8-5c2a5d0c1615"),//3
                    Comment = "Very good shot.",
                    Score = 7,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), //sara
                    PhotoId = Guid.Parse("7780497c-f5e4-43e2-8e90-5067f91475ce"),//the best
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("c625e60d-0daa-4b2f-8c9c-d2733495293b"),//4
                    Comment = "Great timing.",
                    Score = 7,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),//eric
                    PhotoId = Guid.Parse("05505a4d-f749-46e9-928c-039dad92c808"),//winner
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("46687750-d00c-419c-933e-9b8e8d6f1db6"),//4
                    Comment = "Not a very clean shot.",
                    Score = 5,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),//jane
                    PhotoId = Guid.Parse("05505a4d-f749-46e9-928c-039dad92c808"),//winner
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("9bf16f23-4764-4e31-9716-ad756a0761ff"),//5
                    Comment = "Good setting.",
                    Score = 6,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), //sara
                    PhotoId = Guid.Parse("8a178e81-d2c3-44bb-a3d7-79d84f5188b2"),//colloseum
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("e1529b05-cc4d-4eef-9bae-718b5acc4784"),//5
                    Comment = "Quality of the picture is not very good.",
                    Score = 4,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),//jane
                    PhotoId = Guid.Parse("8a178e81-d2c3-44bb-a3d7-79d84f5188b2"),//colloseum
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("96ea7398-6e64-4735-a252-e1a6a18094a7"),//6
                    Comment = "The pyramids are not the center of the picture.",
                    Score = 5,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),//eric
                    PhotoId = Guid.Parse("351b9dbe-4142-4283-bbc4-f90a8a503925"),//pyramids
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("9d520a28-1e1b-4477-9894-e0da97fa982e"),//6
                    Comment = "Not the greatest quality.",
                    Score = 6,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), //sara
                    PhotoId = Guid.Parse("351b9dbe-4142-4283-bbc4-f90a8a503925"),//pyramids
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("3969dc5b-b149-41aa-a17c-b5ec903aa264"),//7
                    Comment = "Unique setting",
                    Score = 8,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),//jane
                    PhotoId = Guid.Parse("08f2b9a9-37a8-4852-86c1-4058a62848a9"),//erasmusburg
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("958d719a-7112-4116-bdbb-5eb6f374f4cf"),//7
                    Comment = "Spectacular shot",
                    Score = 10,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),//eric
                    PhotoId = Guid.Parse("08f2b9a9-37a8-4852-86c1-4058a62848a9"),//erasmusburg
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("2ab3c5bb-a6fc-4c08-b9d6-3e093d4a20f0"),//8
                    Comment = "Very nice colors.",
                    Score = 8,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), //sara
                    PhotoId = Guid.Parse("2b7fb35a-9840-4d4b-b86e-a5e3040117e4"),//goldengate
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("6c104a76-98ee-4bc5-9cd9-fde937d155e9"),//8
                    Comment = "Marvelous shot.",
                    Score = 6,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),//jane
                    PhotoId = Guid.Parse("2b7fb35a-9840-4d4b-b86e-a5e3040117e4"),//goldengate
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("df3841f6-5e8c-4cfe-88eb-09b0e0daaa35"),//9
                    Comment = "Great place, would love to be there.",
                    Score = 10,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),//eric
                    PhotoId = Guid.Parse("723b8f6d-95de-487f-a4b2-52f8be99ce11"),//palm house
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("9c211c22-7b53-4395-a6ab-8df8ea3cf774"),//9
                    Comment = "Great shot.",
                    Score = 7,
                    UserId = Guid.Parse("a890fe35-c840-4484-bd80-67dbc94ab581"),//jane
                    PhotoId = Guid.Parse("723b8f6d-95de-487f-a4b2-52f8be99ce11"),//palm house
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("0fb89447-6c1d-4fc7-a93d-29a7c17b5458"),//10
                    Comment = "Picture is taken for very far.",
                    Score = 4,
                    UserId = Guid.Parse("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), //sara
                    PhotoId = Guid.Parse("94da69ad-71c2-4dca-87a9-def9154ec7b0"),//mountain villa
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
                new Review()
                {
                    Id = Guid.Parse("ef107b97-bc8c-44c1-9b54-80744a8e9ec5"),//10
                    Comment = "Nice colors.",
                    Score = 7,
                    UserId = Guid.Parse("e240edfc-64b9-4358-a869-5aadb719e128"),//eric
                    PhotoId = Guid.Parse("94da69ad-71c2-4dca-87a9-def9154ec7b0"),//mountain villa
                    WrongCategory = false,
                    CreatedOn = DateTime.UtcNow
                },
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
                },
                new UserContest()
                {
                    Id = Guid.Parse("5d33bf17-933f-4fd4-93ba-8fa2a7944bd1"),
                    ContestId = Guid.Parse("42541f52-8d30-4828-bf66-4eda82735edd"),//mansions
                    UserId = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb"),//sam
                },
                new UserContest()
                {
                    Id = Guid.Parse("95afb371-52c1-4aa3-85f6-c7341b3155bc"),
                    ContestId = Guid.Parse("42541f52-8d30-4828-bf66-4eda82735edd"),//mansions
                    UserId = Guid.Parse("743f0e66-af28-48b9-8322-61395c10207f"),//steven
                },
                new UserContest()
                {
                    Id = Guid.Parse("6b50dbde-50d9-4b5c-9425-1ea4c97a4146"),
                    ContestId = Guid.Parse("f9f89a56-448a-43c4-a098-fe5b13605999"),//bridges
                    UserId = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76"),//kyle
                },
                new UserContest()
                {
                    Id = Guid.Parse("3ada8ce7-5c5d-4892-a676-b9d035de1d42"),
                    ContestId = Guid.Parse("f9f89a56-448a-43c4-a098-fe5b13605999"),//bridges
                    UserId = Guid.Parse("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"),//georgi
                },
                new UserContest()
                {
                    Id = Guid.Parse("d8343994-bcae-4096-ba78-d8b669be0049"),
                    ContestId = Guid.Parse("d7b46312-7197-4c79-8384-1ec2b8577f8d"),//landmarks
                    UserId = Guid.Parse("021fa300-ffd4-48e2-a93f-d40c17d014f3"),//john
                },
                new UserContest()
                {
                    Id = Guid.Parse("863aa575-bc89-4de7-9895-ff8d4f70fea8"),
                    ContestId = Guid.Parse("d7b46312-7197-4c79-8384-1ec2b8577f8d"),//landmarks
                    UserId = Guid.Parse("743f0e66-af28-48b9-8322-61395c10207f"),//steven
                },
                new UserContest()
                {
                    Id = Guid.Parse("d10fb8e0-d659-4cd9-8da9-d84b98e4e8f1"),
                    ContestId = Guid.Parse("d2acdd9c-9427-4fc2-897e-5f52da2190dc"),//olympics
                    UserId = Guid.Parse("56763358-b113-4f96-9a4a-5190c421f1fb"),//sam
                },
                new UserContest()
                {
                    Id = Guid.Parse("00aa09e3-43d0-46b0-a213-45d4a72a7b4c"),
                    ContestId = Guid.Parse("d2acdd9c-9427-4fc2-897e-5f52da2190dc"),//olympics
                    UserId = Guid.Parse("71cd9097-0c95-4af2-9e43-da7324880583"),//robert
                },
                new UserContest()
                {
                    Id = Guid.Parse("ed1dd103-4469-4699-ba7e-59b14805a8a8"),
                    ContestId = Guid.Parse("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"),//sunsets
                    UserId = Guid.Parse("7cc9804e-2106-4943-994d-91be3d1fab8e"),//jimmy
                },
                new UserContest()
                {
                    Id = Guid.Parse("0ea3f579-a3ec-499e-82d8-35ab5c0af28f"),
                    ContestId = Guid.Parse("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"),//sunsets
                    UserId = Guid.Parse("c463712b-e235-4fe5-840e-a99736c3fb76"),//kyle
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

