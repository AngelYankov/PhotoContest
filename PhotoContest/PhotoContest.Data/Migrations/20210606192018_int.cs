using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoContest.Data.Migrations
{
    public partial class @int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    RankId = table.Column<Guid>(nullable: false),
                    OverallPoints = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Ranks_RankId",
                        column: x => x.RankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    IsOpen = table.Column<bool>(nullable: false),
                    Phase1 = table.Column<DateTime>(nullable: false),
                    Phase2 = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: false),
                    IsCalculated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contests_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contests_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Juries",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    ContestId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juries", x => new { x.UserId, x.ContestId });
                    table.ForeignKey(
                        name: "FK_Juries_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Juries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    PhotoUrl = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ContestId = table.Column<Guid>(nullable: false),
                    AllPoints = table.Column<double>(nullable: false),
                    IsInWrongCategory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserContests",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    ContestId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    IsAdded = table.Column<bool>(nullable: false),
                    IsInvited = table.Column<bool>(nullable: false),
                    HasUploadedPhoto = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContests", x => new { x.ContestId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserContests_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserContests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(maxLength: 50, nullable: false),
                    Score = table.Column<double>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PhotoId = table.Column<Guid>(nullable: false),
                    WrongCategory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a117e076-855e-401a-aeab-844fee43a0a2"), "f75b7d8b-bc11-483e-8023-1efc192ad972", "User", "USER" },
                    { new Guid("8a73d7c7-c092-4281-8cde-6dd9a9dd747c"), "8414baf5-3eef-4bc1-bade-849201cc432b", "Organizer", "ORGANIZER" },
                    { new Guid("d0a458f4-cba3-4e49-a779-f79a9de41268"), "e4251b98-beea-474d-85ba-ffe2888529f9", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("28f87c5a-b02a-4422-8b71-4821306279d5"), new DateTime(2021, 6, 6, 19, 20, 17, 724, DateTimeKind.Utc).AddTicks(7781), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports" },
                    { new Guid("a8c71a51-79f2-46ef-8a88-7983cbb7259a"), new DateTime(2021, 6, 6, 19, 20, 17, 724, DateTimeKind.Utc).AddTicks(7779), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "People" },
                    { new Guid("e43364e1-28d4-48c3-b4e2-5bd5f43b89b0"), new DateTime(2021, 6, 6, 19, 20, 17, 724, DateTimeKind.Utc).AddTicks(7387), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cars" },
                    { new Guid("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), new DateTime(2021, 6, 6, 19, 20, 17, 724, DateTimeKind.Utc).AddTicks(7766), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architecture" },
                    { new Guid("ad729c24-eda2-4209-93c8-f80d1f47172c"), new DateTime(2021, 6, 6, 19, 20, 17, 724, DateTimeKind.Utc).AddTicks(7762), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nature" },
                    { new Guid("729b970a-ee54-4852-8ac7-d9b3146e886b"), new DateTime(2021, 6, 6, 19, 20, 17, 724, DateTimeKind.Utc).AddTicks(7740), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Animals" },
                    { new Guid("fad09db4-8187-4777-9e68-3ba40218c7d3"), new DateTime(2021, 6, 6, 19, 20, 17, 724, DateTimeKind.Utc).AddTicks(7773), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Motorcycles" }
                });

            migrationBuilder.InsertData(
                table: "Ranks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), "Organizer" },
                    { new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "Junkie" },
                    { new Guid("41c8e397-f768-48ed-b8f1-f8a238c739b1"), "Enthusiast" },
                    { new Guid("a9576301-3157-454f-86ce-85bb5eb2dfc9"), "Master" },
                    { new Guid("0b1728c7-5582-4958-9e97-52c9b1d44cdb"), "Wise and Benevolent Photo Dictator" },
                    { new Guid("a036e464-8996-4e40-9a81-39239cf72402"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"), "Phase 1" },
                    { new Guid("27c7d81e-eb1c-469b-8919-a532322273cc"), "Phase 2" },
                    { new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"), "Finished" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "OverallPoints", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RankId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1d4c48e4-8870-417b-8ac6-e78efe1aaab5"), 0, "df8712f9-2366-47b1-bdf9-caf5a4bfc331", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(6722), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@mail.com", false, "Admin", false, "Admin", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", 0, "AQAAAAEAACcQAAAAEDWUbxBKGcnTvCvbgBE9qC5huf5b0EiytM3xRtSL+EUkpbXtF3yypQiwLAYafs1a4A==", null, false, new Guid("a036e464-8996-4e40-9a81-39239cf72402"), "DC6E275DD1E24957A7781D42BB68299B", false, "admin@mail.com" },
                    { new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), 0, "8a887aa2-b507-4d79-b6bf-baa6e793b97b", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8477), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.smith@mail.com", false, "Sara", false, "Smith", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SARA.SMITH@MAIL.COM", "SARA.SMITH@MAIL.COM", 0, "AQAAAAEAACcQAAAAEM7PdL70Ricw7ov/UXwC4p9PFakUGDV4he2M05TyPZqn0DRmzSu/gLLKvjhxCiVurQ==", null, false, new Guid("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), "DC6E275DD1E24917A7781D42BB68293B", false, "sara.smith@mail.com" },
                    { new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), 0, "c0a0438f-f8b9-4cf1-a90b-c79ed4e77beb", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8007), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "eric.berg@mail.com", false, "Eric", false, "Berg", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ERIC.BERG@MAIL.COM", "ERIC.BERG@MAIL.COM", 0, "AQAAAAEAACcQAAAAEDdqOESNZZ+xZrJbZ8AsumrSaJbHV2+Fk1VNkm50HkivaZo4tXyTKob3AgxvOPKO/g==", null, false, new Guid("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), "DC6E275DD1E24957A7781D42BB68293B", false, "eric.berg@mail.com" },
                    { new Guid("c463712b-e235-4fe5-840e-a99736c3fb76"), 0, "b925c407-5321-4f6a-a6a9-e77d0799c5a6", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8453), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kyle.sins@mail.com", false, "Kyle", false, "Sins", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KYLE.SINS@MAIL.COM", "KYLE.SINS@MAIL.COM", 1200, "AQAAAAEAACcQAAAAEG4djghV0LA07bOzOp/PJeESnmSj1LIqruEudZCw5wSuEBPbnNQ9KpjOPZHhBjiF6g==", null, false, new Guid("0b1728c7-5582-4958-9e97-52c9b1d44cdb"), "DC6E375DD2E25957A7981D48BB68399B", false, "kyle.sins@mail.com" },
                    { new Guid("56763358-b113-4f96-9a4a-5190c421f1fb"), 0, "a08e691a-a22d-4754-95af-619bcf021ac7", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8153), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sam.stevens@mail.com", false, "Sam", false, "Stevens", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SAM.STEVENS@MAIL.COM", "SAM.STEVENS@MAIL.COM", 200, "AQAAAAEAACcQAAAAEMpG7BU5anZOvzUq0pQ5PAtGmaBKGB9fCBBVihPxXDmk79MZ7A2HtkJIuqXaqh/Acg==", null, false, new Guid("a9576301-3157-454f-86ce-85bb5eb2dfc9"), "DC6E375DD1E25957A7981D48BB68399B", false, "sam.stevens@mail.com" },
                    { new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e"), 0, "14716ff3-22dc-4122-8302-12c7bc490f8f", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8145), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jimmy.brown@mail.com", false, "Jimmy", false, "Brown", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JIMMY.BROWN@MAIL.COM", "JIMMY.BROWN@MAIL.COM", 0, "AQAAAAEAACcQAAAAEP1qQSMKzFMilGxznm4gZi8a0D+fUvx906cTCEDT5WAi0m4NhBdzyRhtwna8bv3HZA==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E375DD1E25957A7981D42BB68399B", false, "jimmy.brown@mail.com" },
                    { new Guid("71cd9097-0c95-4af2-9e43-da7324880583"), 0, "3e453e3d-f401-4531-a89d-a437e7b81709", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8138), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "robert.scott@mail.com", false, "Robert", false, "Scott", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ROBERT.SCOTT@MAIL.COM", "ROBERT.SCOTT@MAIL.COM", 0, "AQAAAAEAACcQAAAAEOjVjJ2o52gSutboYMz3wC4hHsrVl9QCcLyre9Hhic8a3lxjuS9fa8aslaf9ym/FsQ==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E375DD1E25957A7981D42BB68299B", false, "robert.scott@mail.com" },
                    { new Guid("743f0e66-af28-48b9-8322-61395c10207f"), 0, "53149cd3-c9f2-4f3b-a187-d3cede54374a", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8120), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "steven.king@mail.com", false, "Steven", false, "King", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "STEVEN.KING@MAIL.COM", "STEVEN.KING@MAIL.COM", 0, "AQAAAAEAACcQAAAAEPTX7Tk5RZRAzQJnbk6NfBTkSuMF6uLiot12CL102Pb1UpeukO8EYznwM10vB1+7fw==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E375DD1E25957A7781D42BB68299B", false, "steven.king@mail.com" },
                    { new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3"), 0, "9b716958-099c-4a82-b97b-b997d9f7b954", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8111), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@mail.com", false, "John", false, "Smith", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JOHN.SMITH@MAIL.COM", "JOHN.SMITH@MAIL.COM", 0, "AQAAAAEAACcQAAAAEOp5KzFfBeqm4Q3eW1Yx0+Y4AUxtkFZqwMCTMGxHWTsfgdRXOYMiQfTH/B1pGJZNfg==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E275DD1E25957A7781D42BB68299B", false, "john.smith@mail.com" },
                    { new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"), 0, "a30aa4a5-9c7c-4895-a76d-bfbb92b72f61", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8035), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "georgi.ivanov@mail.com", false, "Georgi", false, "Ivanov", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GEORGI.IVANOV@MAIL.COM", "GEORGI.IVANOV@MAIL.COM", 0, "AQAAAAEAACcQAAAAEElHVovP/sMOaXGCJEdagE8FGEX6eWXoLlXUmmwvnIrBdE3Bj0jVK3amJM8MxrI04w==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E275DD1E24957A7781D42BB68292B", false, "georgi.ivanov@mail.com" },
                    { new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), 0, "7c5257e8-c8a4-462c-83be-f081b94a51ca", new DateTime(2021, 6, 6, 19, 20, 17, 647, DateTimeKind.Utc).AddTicks(8486), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.beck@mail.com", false, "Jane", false, "Beck", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JANE.BECK@MAIL.COM", "JANE.BECK@MAIL.COM", 0, "AQAAAAEAACcQAAAAEErRzV0DKLwpChOYaniko/JzeQ+4wdnz5sbmIaIi7Mz+3uvxXOROjF/OvjO9vyRkIA==", null, false, new Guid("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), "DC6E275DD1E24917A7781D42BB64293B", false, "jane.beck@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Contests",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "DeletedOn", "Finished", "IsCalculated", "IsDeleted", "IsOpen", "ModifiedOn", "Name", "Phase1", "Phase2", "StatusId" },
                values: new object[,]
                {
                    { new Guid("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"), new Guid("ad729c24-eda2-4209-93c8-f80d1f47172c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sunsets", new DateTime(2021, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6") },
                    { new Guid("d2acdd9c-9427-4fc2-897e-5f52da2190dc"), new Guid("28f87c5a-b02a-4422-8b71-4821306279d5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Olympics", new DateTime(2021, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6") },
                    { new Guid("f9f89a56-448a-43c4-a098-fe5b13605999"), new Guid("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bridges", new DateTime(2021, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6") },
                    { new Guid("42541f52-8d30-4828-bf66-4eda82735edd"), new Guid("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mansions", new DateTime(2021, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6") },
                    { new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new Guid("fad09db4-8187-4777-9e68-3ba40218c7d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 9, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Best look", new DateTime(2021, 5, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6") },
                    { new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new Guid("729b970a-ee54-4852-8ac7-d9b3146e886b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 7, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(2993), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wild cats", new DateTime(2021, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(2992), new Guid("27c7d81e-eb1c-469b-8919-a532322273cc") },
                    { new Guid("17578ed3-fdfc-4616-9cdb-55a5ff762caf"), new Guid("a8c71a51-79f2-46ef-8a88-7983cbb7259a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 18, 19, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Couples", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(2985), new DateTime(2021, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186") },
                    { new Guid("c5d76f38-f3f9-4aaa-931f-0d91e0207a0a"), new Guid("ad729c24-eda2-4209-93c8-f80d1f47172c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 17, 19, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lakes", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(2977), new DateTime(2021, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186") },
                    { new Guid("b2ba5698-bc3a-45ee-8dcb-06e6a2e99a09"), new Guid("e43364e1-28d4-48c3-b4e2-5bd5f43b89b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 16, 19, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Muscle cars", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(2971), new DateTime(2021, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186") },
                    { new Guid("fd4ec4c7-3976-4a44-9dfa-06967ab471c1"), new Guid("729b970a-ee54-4852-8ac7-d9b3146e886b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Snakes", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(2964), new DateTime(2021, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186") },
                    { new Guid("f8d22e50-664b-483f-a542-ab26135e6772"), new Guid("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 14, 19, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Skyscrapers", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(2934), new DateTime(2021, 6, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186") },
                    { new Guid("d7b46312-7197-4c79-8384-1ec2b8577f8d"), new Guid("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historic landmarks", new DateTime(2021, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6") },
                    { new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new Guid("729b970a-ee54-4852-8ac7-d9b3146e886b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birds", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(2069), new DateTime(2021, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186") }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") },
                    { new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), new Guid("8a73d7c7-c092-4281-8cde-6dd9a9dd747c") },
                    { new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), new Guid("8a73d7c7-c092-4281-8cde-6dd9a9dd747c") },
                    { new Guid("c463712b-e235-4fe5-840e-a99736c3fb76"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") },
                    { new Guid("56763358-b113-4f96-9a4a-5190c421f1fb"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") },
                    { new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), new Guid("8a73d7c7-c092-4281-8cde-6dd9a9dd747c") },
                    { new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") },
                    { new Guid("71cd9097-0c95-4af2-9e43-da7324880583"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") },
                    { new Guid("743f0e66-af28-48b9-8322-61395c10207f"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") },
                    { new Guid("1d4c48e4-8870-417b-8ac6-e78efe1aaab5"), new Guid("d0a458f4-cba3-4e49-a779-f79a9de41268") },
                    { new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "AllPoints", "ContestId", "CreatedOn", "DeletedOn", "Description", "IsDeleted", "IsInWrongCategory", "ModifiedOn", "PhotoUrl", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"), 0.0, new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5143), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Going for the kill.", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/da80d3a0-2aaa-4360-871e-699b5507277f_tiger1.jpg", "Tiger", new Guid("56763358-b113-4f96-9a4a-5190c421f1fb") },
                    { new Guid("e165b91f-03bf-414e-88b7-c51b87775683"), 0.0, new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5058), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roaring of a lion.", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/c4baabc4-bd02-4bd6-bb33-955556530c8e_lion1.jpg", "Lion King", new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d") },
                    { new Guid("08f2b9a9-37a8-4852-86c1-4058a62848a9"), 0.0, new Guid("f9f89a56-448a-43c4-a098-fe5b13605999"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5209), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Man made beauty", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/b364f4aa-a6af-4570-966f-f1980c6eb636_bridges1.jpeg", "Erasmusbrug", new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d") },
                    { new Guid("2b7fb35a-9840-4d4b-b86e-a5e3040117e4"), 0.0, new Guid("f9f89a56-448a-43c4-a098-fe5b13605999"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5213), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering genius", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/52dc2b1e-112c-4914-98b9-13137abd989a_bridges2.jpeg", "Goldengate", new Guid("c463712b-e235-4fe5-840e-a99736c3fb76") },
                    { new Guid("5edcd3ee-3033-43fe-a222-97fdac5922ab"), 0.0, new Guid("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5176), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beautiful sunset in Kenya", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/ccf1a5e0-b616-4e99-a524-6691e79ca16b_sunset1.jpg", "African sunset", new Guid("c463712b-e235-4fe5-840e-a99736c3fb76") },
                    { new Guid("507c5f65-497b-4a3c-95f6-cfbc86692ca5"), 0.0, new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5156), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Takaing a small break from the road.", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/dcc53eb5-2024-4242-877c-423e9c0d751f_honda1.jpg", "Honda CBR", new Guid("c463712b-e235-4fe5-840e-a99736c3fb76") },
                    { new Guid("94da69ad-71c2-4dca-87a9-def9154ec7b0"), 0.0, new Guid("42541f52-8d30-4828-bf66-4eda82735edd"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5224), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nothing like fresh mountain air", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/c77ff246-1712-4ab3-84cf-15b25966aebd_mansions2.jpg", "Mountain villa", new Guid("56763358-b113-4f96-9a4a-5190c421f1fb") },
                    { new Guid("7780497c-f5e4-43e2-8e90-5067f91475ce"), 0.0, new Guid("d2acdd9c-9427-4fc2-897e-5f52da2190dc"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5186), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Usain Bolt breaking the record", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/cb2acc2c-2576-4707-86ae-754e983e1f55_olympics1.jpg", "The best", new Guid("56763358-b113-4f96-9a4a-5190c421f1fb") },
                    { new Guid("44e5ce67-461c-4082-9acb-c5aceae13a0c"), 0.0, new Guid("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5181), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sunset over the Statue of Liberty", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/7ef91ce8-58f5-416d-a8c2-56d0218e6ebf_sunset2.jpg", "New York sunset", new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e") },
                    { new Guid("59dd9540-a1d8-4360-99d5-ed8302aae5e2"), 0.0, new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5163), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hummingbird on a branch.", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/3a425bea-85d7-4473-850b-afb9162dfe7e_colibri1.jpg", "Hummingbird", new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e") },
                    { new Guid("8a178e81-d2c3-44bb-a3d7-79d84f5188b2"), 0.0, new Guid("d7b46312-7197-4c79-8384-1ec2b8577f8d"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5198), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The mighty arena of gladiators", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/a6e65cfd-81dd-4c64-aa05-11946f97b102_landmarks1.jpg", "The Colloseum", new Guid("743f0e66-af28-48b9-8322-61395c10207f") },
                    { new Guid("723b8f6d-95de-487f-a4b2-52f8be99ce11"), 0.0, new Guid("42541f52-8d30-4828-bf66-4eda82735edd"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5218), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greatest place to lose time", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/d8f9b48e-7743-48fe-8e38-ba06e979716e_mansions1.jpg", "Beach house", new Guid("743f0e66-af28-48b9-8322-61395c10207f") },
                    { new Guid("05505a4d-f749-46e9-928c-039dad92c808"), 0.0, new Guid("d2acdd9c-9427-4fc2-897e-5f52da2190dc"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5193), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael Phelps winning gold", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/71a3e79d-ee0e-4d4b-9a03-2890c42be96a_olympics2.jpg", "Winner", new Guid("71cd9097-0c95-4af2-9e43-da7324880583") },
                    { new Guid("94499cdd-e18c-4743-b0c4-2e1b7564c46c"), 0.0, new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5171), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scanning for prey.", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/1d2dfd54-7d9b-41c4-b442-995baa1ac289_eagle1.jpg", "Eagle", new Guid("71cd9097-0c95-4af2-9e43-da7324880583") },
                    { new Guid("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"), 0.0, new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5150), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Parked for the day.", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/16bb1fa0-8f61-4717-bba1-e14f8c47b616_kawasaki1.jpg", "Kawasaki Ninja", new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3") },
                    { new Guid("351b9dbe-4142-4283-bbc4-f90a8a503925"), 0.0, new Guid("d7b46312-7197-4c79-8384-1ec2b8577f8d"), new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(5203), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The last standing wonder of the old world", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/a562ac79-7f5e-48e7-a364-43cc656227bb_landmarks2.jpg", "The Pyramids", new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3") }
                });

            migrationBuilder.InsertData(
                table: "UserContests",
                columns: new[] { "ContestId", "UserId", "HasUploadedPhoto", "Id", "IsAdded", "IsInvited", "Points" },
                values: new object[,]
                {
                    { new Guid("d2acdd9c-9427-4fc2-897e-5f52da2190dc"), new Guid("71cd9097-0c95-4af2-9e43-da7324880583"), false, new Guid("00aa09e3-43d0-46b0-a213-45d4a72a7b4c"), false, false, 0 },
                    { new Guid("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"), new Guid("c463712b-e235-4fe5-840e-a99736c3fb76"), false, new Guid("0ea3f579-a3ec-499e-82d8-35ab5c0af28f"), false, false, 0 },
                    { new Guid("f9f89a56-448a-43c4-a098-fe5b13605999"), new Guid("c463712b-e235-4fe5-840e-a99736c3fb76"), false, new Guid("6b50dbde-50d9-4b5c-9425-1ea4c97a4146"), false, false, 0 },
                    { new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new Guid("c463712b-e235-4fe5-840e-a99736c3fb76"), false, new Guid("f933eff8-9a79-4937-801a-a80aaa8d4b19"), false, false, 0 },
                    { new Guid("42541f52-8d30-4828-bf66-4eda82735edd"), new Guid("743f0e66-af28-48b9-8322-61395c10207f"), false, new Guid("95afb371-52c1-4aa3-85f6-c7341b3155bc"), false, false, 0 },
                    { new Guid("d7b46312-7197-4c79-8384-1ec2b8577f8d"), new Guid("743f0e66-af28-48b9-8322-61395c10207f"), false, new Guid("863aa575-bc89-4de7-9895-ff8d4f70fea8"), false, false, 0 },
                    { new Guid("d2acdd9c-9427-4fc2-897e-5f52da2190dc"), new Guid("56763358-b113-4f96-9a4a-5190c421f1fb"), false, new Guid("d10fb8e0-d659-4cd9-8da9-d84b98e4e8f1"), false, false, 0 },
                    { new Guid("42541f52-8d30-4828-bf66-4eda82735edd"), new Guid("56763358-b113-4f96-9a4a-5190c421f1fb"), false, new Guid("5d33bf17-933f-4fd4-93ba-8fa2a7944bd1"), false, false, 0 },
                    { new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new Guid("56763358-b113-4f96-9a4a-5190c421f1fb"), false, new Guid("61f45846-09fd-4112-b3b8-5aaf029e8a9f"), false, false, 0 },
                    { new Guid("d7b46312-7197-4c79-8384-1ec2b8577f8d"), new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3"), false, new Guid("d8343994-bcae-4096-ba78-d8b669be0049"), false, false, 0 },
                    { new Guid("f9f89a56-448a-43c4-a098-fe5b13605999"), new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"), false, new Guid("3ada8ce7-5c5d-4892-a676-b9d035de1d42"), false, false, 0 },
                    { new Guid("06e8bf71-fc93-42ff-8c99-a5265a8ea2e9"), new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e"), false, new Guid("ed1dd103-4469-4699-ba7e-59b14805a8a8"), false, false, 0 },
                    { new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e"), false, new Guid("1e1008e0-63f6-437a-8c86-347dcf905b7d"), false, false, 0 },
                    { new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new Guid("71cd9097-0c95-4af2-9e43-da7324880583"), false, new Guid("bb047135-03e9-4957-8248-306eaf8600cc"), false, false, 0 },
                    { new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"), false, new Guid("be9c8856-5df8-4577-a7c9-f8f62f8de22c"), false, false, 0 },
                    { new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3"), false, new Guid("d00fb4ba-c05c-4a48-8042-0db3b747b226"), false, false, 0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "PhotoId", "Score", "UserId", "WrongCategory" },
                values: new object[,]
                {
                    { new Guid("f55244de-da0f-4a9c-b8d9-7940a2f97083"), "Great angle.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(6915), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e165b91f-03bf-414e-88b7-c51b87775683"), 6.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("a15576fd-9e09-4b2a-b961-60b9204e803b"), "Unique colors.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7038), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5edcd3ee-3033-43fe-a222-97fdac5922ab"), 8.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("18d39ed7-120f-4239-8920-b0b826dd3d0a"), "Mesmerizing shot.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7033), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5edcd3ee-3033-43fe-a222-97fdac5922ab"), 10.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false },
                    { new Guid("ef107b97-bc8c-44c1-9b54-80744a8e9ec5"), "Nice colors.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7126), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94da69ad-71c2-4dca-87a9-def9154ec7b0"), 7.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("0fb89447-6c1d-4fc7-a93d-29a7c17b5458"), "Picture is taken for very far.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7122), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94da69ad-71c2-4dca-87a9-def9154ec7b0"), 4.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false },
                    { new Guid("d9beb14d-56c8-42d1-a6b8-5c2a5d0c1615"), "Very good shot.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7058), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7780497c-f5e4-43e2-8e90-5067f91475ce"), 7.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false },
                    { new Guid("98deadcb-ffb0-47c7-ac06-4dd5c8fc22fa"), "Great quality.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7053), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7780497c-f5e4-43e2-8e90-5067f91475ce"), 8.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("b8e942fb-9e23-48b2-b15f-32a1e2c06315"), "Shadows are in the way.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7021), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"), 3.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false },
                    { new Guid("73fe1a7a-e31c-4b4e-a6fa-1ae65e7e1f28"), "Perfect shot.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7015), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"), 10.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("7e2c2b98-9616-479a-bd1f-61d0510ccb15"), "Great perspective.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7047), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("44e5ce67-461c-4082-9acb-c5aceae13a0c"), 8.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("5405c1fb-ab2e-4ada-95f2-c37436d3e24c"), "Not really shot of a sunset.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7043), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("44e5ce67-461c-4082-9acb-c5aceae13a0c"), 5.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("2ab3c5bb-a6fc-4c08-b9d6-3e093d4a20f0"), "Very nice colors.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7102), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b7fb35a-9840-4d4b-b86e-a5e3040117e4"), 8.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false },
                    { new Guid("46687750-d00c-419c-933e-9b8e8d6f1db6"), "Not a very clean shot.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7068), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("05505a4d-f749-46e9-928c-039dad92c808"), 5.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("9c211c22-7b53-4395-a6ab-8df8ea3cf774"), "Great shot.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7116), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("723b8f6d-95de-487f-a4b2-52f8be99ce11"), 7.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("df3841f6-5e8c-4cfe-88eb-09b0e0daaa35"), "Great place, would love to be there.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7111), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("723b8f6d-95de-487f-a4b2-52f8be99ce11"), 10.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("e1529b05-cc4d-4eef-9bae-718b5acc4784"), "Quality of the picture is not very good.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7078), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a178e81-d2c3-44bb-a3d7-79d84f5188b2"), 4.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("9bf16f23-4764-4e31-9716-ad756a0761ff"), "Good setting.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7073), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a178e81-d2c3-44bb-a3d7-79d84f5188b2"), 6.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false },
                    { new Guid("9d520a28-1e1b-4477-9894-e0da97fa982e"), "Not the greatest quality.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7087), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("351b9dbe-4142-4283-bbc4-f90a8a503925"), 6.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false },
                    { new Guid("96ea7398-6e64-4735-a252-e1a6a18094a7"), "The pyramids are not the center of the picture.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7082), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("351b9dbe-4142-4283-bbc4-f90a8a503925"), 5.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("55cf8205-bfb9-4d8c-8ac1-7861a458bb10"), "Great shot.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7026), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"), 9.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("958d719a-7112-4116-bdbb-5eb6f374f4cf"), "Spectacular shot", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7097), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("08f2b9a9-37a8-4852-86c1-4058a62848a9"), 10.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("3969dc5b-b149-41aa-a17c-b5ec903aa264"), "Unique setting", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7092), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("08f2b9a9-37a8-4852-86c1-4058a62848a9"), 8.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("8198e13a-30cb-4f4b-99f0-acf31a70b02d"), "Perfect timing.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7008), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e165b91f-03bf-414e-88b7-c51b87775683"), 8.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("c625e60d-0daa-4b2f-8c9c-d2733495293b"), "Great timing.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7064), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("05505a4d-f749-46e9-928c-039dad92c808"), 7.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("6c104a76-98ee-4bc5-9cd9-fde937d155e9"), "Marvelous shot.", new DateTime(2021, 6, 6, 19, 20, 17, 725, DateTimeKind.Utc).AddTicks(7106), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b7fb35a-9840-4d4b-b86e-a5e3040117e4"), 6.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RankId",
                table: "AspNetUsers",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contests_CategoryId",
                table: "Contests",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contests_Name",
                table: "Contests",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contests_StatusId",
                table: "Contests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Juries_ContestId",
                table: "Juries",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ContestId",
                table: "Photos",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PhotoId",
                table: "Reviews",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContests_UserId",
                table: "UserContests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Juries");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserContests");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Ranks");
        }
    }
}
