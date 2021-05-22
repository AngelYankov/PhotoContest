﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhotoContest.Data;

namespace PhotoContest.Data.Migrations
{
    [DbContext(typeof(PhotoContestContext))]
    [Migration("20210522141116_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("c3c9cbd9-9956-47b9-a644-13868d6294c3"),
                            RoleId = new Guid("113e2d83-5fd3-472d-bb02-4a8bc262e138")
                        },
                        new
                        {
                            UserId = new Guid("e461cf5e-b272-4f2b-9894-a7d5912c9de4"),
                            RoleId = new Guid("a92f4ab6-422e-4de5-a408-12fbf4f19b48")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PhotoContest.Data.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("928968ba-453e-4e8a-901e-38857cf22e88"),
                            CreatedOn = new DateTime(2021, 5, 22, 14, 11, 15, 385, DateTimeKind.Utc).AddTicks(1748),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Cars"
                        },
                        new
                        {
                            Id = new Guid("04f798de-0202-4b10-bde0-273582c68398"),
                            CreatedOn = new DateTime(2021, 5, 22, 14, 11, 15, 385, DateTimeKind.Utc).AddTicks(2333),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Animals"
                        },
                        new
                        {
                            Id = new Guid("39b818c3-7cb1-4dd3-9db7-3d5fec1513e6"),
                            CreatedOn = new DateTime(2021, 5, 22, 14, 11, 15, 385, DateTimeKind.Utc).AddTicks(2346),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Nature"
                        },
                        new
                        {
                            Id = new Guid("df7bce85-30ce-446d-8760-e3108fb50499"),
                            CreatedOn = new DateTime(2021, 5, 22, 14, 11, 15, 385, DateTimeKind.Utc).AddTicks(2349),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Architecture"
                        },
                        new
                        {
                            Id = new Guid("8aa00d0b-56dd-456b-8969-57c82a6c0ddd"),
                            CreatedOn = new DateTime(2021, 5, 22, 14, 11, 15, 385, DateTimeKind.Utc).AddTicks(2361),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Motorbikes"
                        });
                });

            modelBuilder.Entity("PhotoContest.Data.Models.Contest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Finished")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("Open")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Phase1")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Phase2")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("StatusId");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("PhotoContest.Data.Models.JuryMember", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "ContestId");

                    b.HasIndex("ContestId");

                    b.ToTable("Juries");
                });

            modelBuilder.Entity("PhotoContest.Data.Models.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("PhotoContest.Data.Models.PhotoRating", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("PhotoId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PhotoRatings");
                });

            modelBuilder.Entity("PhotoContest.Data.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("113e2d83-5fd3-472d-bb02-4a8bc262e138"),
                            ConcurrencyStamp = "e2c405e5-18ba-4e7c-8b12-3dde9bf82e89",
                            Name = "Organizer",
                            NormalizedName = "ORGANIZER"
                        },
                        new
                        {
                            Id = new Guid("a92f4ab6-422e-4de5-a408-12fbf4f19b48"),
                            ConcurrencyStamp = "d0bb392c-badf-4566-90e4-d82a6be449c8",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("PhotoContest.Data.Models.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("61a2d801-edac-45df-8d20-880c84c949f5"),
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Phase 1"
                        },
                        new
                        {
                            Id = new Guid("16ef8f0a-ff6c-4ff3-870a-2b1f43e1a205"),
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Phase 2"
                        },
                        new
                        {
                            Id = new Guid("5fabf41a-9527-4dd5-a6f8-df454d3abcd8"),
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Finished"
                        });
                });

            modelBuilder.Entity("PhotoContest.Data.Models.UserContest", b =>
                {
                    b.Property<Guid>("ContestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("ContestId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContests");
                });

            modelBuilder.Entity("PhotoContest.Data.Rank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ranks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7ed3b37b-af69-4923-a964-bd00e9bec491"),
                            Name = "Junkie"
                        },
                        new
                        {
                            Id = new Guid("bb905318-872a-4ceb-916e-765125e2b126"),
                            Name = "Enthusiast"
                        },
                        new
                        {
                            Id = new Guid("279b57c5-347f-453c-8c17-34009a117655"),
                            Name = "Master"
                        },
                        new
                        {
                            Id = new Guid("30d51bd8-b194-4403-945c-1fb6667b1b8a"),
                            Name = "Wise and Benevolent Photo Dictator"
                        },
                        new
                        {
                            Id = new Guid("5fa356f1-c460-45a3-b91c-4ef728e2939d"),
                            Name = "Organizer"
                        });
                });

            modelBuilder.Entity("PhotoContest.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("OverallPoints")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RankId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c3c9cbd9-9956-47b9-a644-13868d6294c3"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6b550dd0-ebc7-43c1-b605-9de091bd8570",
                            CreatedOn = new DateTime(2021, 5, 22, 14, 11, 15, 387, DateTimeKind.Utc).AddTicks(5101),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "eric.berg@mail.com",
                            EmailConfirmed = false,
                            FirstName = "Eric",
                            IsDeleted = false,
                            LastName = "Berg",
                            LockoutEnabled = true,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NormalizedEmail = "ERIC.BERG@MAIL.COM",
                            NormalizedUserName = "ERIC.BERG@MAIL.COM",
                            OverallPoints = 0,
                            PasswordHash = "AQAAAAEAACcQAAAAEKcr2SuqdanU9oNdDJCzaDflInigVfcQk8XnEvW9xaexn3IDMJ7lSVm51rz6RYYasQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "DC6E275DD1E24957A7781D42BB68293B",
                            TwoFactorEnabled = false,
                            UserName = "eric.berg@mail.com"
                        },
                        new
                        {
                            Id = new Guid("e461cf5e-b272-4f2b-9894-a7d5912c9de4"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4a775437-0799-4eae-9e33-cbca46c9decc",
                            CreatedOn = new DateTime(2021, 5, 22, 14, 11, 15, 396, DateTimeKind.Utc).AddTicks(8564),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "georgi.ivanov@mail.com",
                            EmailConfirmed = false,
                            FirstName = "Georgi",
                            IsDeleted = false,
                            LastName = "Ivanov",
                            LockoutEnabled = true,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NormalizedEmail = "GEORGI.IVANOV@MAIL.COM",
                            NormalizedUserName = "GEORGI.IVANOV@MAIL.COM",
                            OverallPoints = 0,
                            PasswordHash = "AQAAAAEAACcQAAAAEJjiAxnYYcqu+x7oQCiCOUkO5LKhtZ5koJYFa1zhMJa3Z2DYDohNM+VSBaMr3ouMbA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "DC6E275DD1E24957A7781D42BB68292B",
                            TwoFactorEnabled = false,
                            UserName = "georgi.ivanov@mail.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("PhotoContest.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("PhotoContest.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("PhotoContest.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("PhotoContest.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoContest.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("PhotoContest.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoContest.Data.Models.Contest", b =>
                {
                    b.HasOne("PhotoContest.Data.Models.Category", "Category")
                        .WithMany("Contests")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhotoContest.Data.Models.Status", "Status")
                        .WithMany("Contests")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoContest.Data.Models.JuryMember", b =>
                {
                    b.HasOne("PhotoContest.Data.Models.Contest", "Contest")
                        .WithMany("Juries")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhotoContest.Data.User", "User")
                        .WithMany("Juries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoContest.Data.Models.Photo", b =>
                {
                    b.HasOne("PhotoContest.Data.Models.Contest", "Contest")
                        .WithMany("Photos")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoContest.Data.Models.PhotoRating", b =>
                {
                    b.HasOne("PhotoContest.Data.Models.Photo", "Photo")
                        .WithMany("PhotoRatings")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhotoContest.Data.User", "User")
                        .WithMany("PhotoRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoContest.Data.Models.UserContest", b =>
                {
                    b.HasOne("PhotoContest.Data.Models.Contest", "Contest")
                        .WithMany("UserContests")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhotoContest.Data.User", "User")
                        .WithMany("UserContests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoContest.Data.User", b =>
                {
                    b.HasOne("PhotoContest.Data.Rank", "Rank")
                        .WithMany("Users")
                        .HasForeignKey("RankId");
                });
#pragma warning restore 612, 618
        }
    }
}
