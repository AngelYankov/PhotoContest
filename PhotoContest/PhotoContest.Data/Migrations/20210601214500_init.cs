using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoContest.Data.Migrations
{
    public partial class init : Migration
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
                    Finished = table.Column<DateTime>(nullable: false)
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
                    Title = table.Column<string>(maxLength: 15, nullable: false),
                    Description = table.Column<string>(maxLength: 30, nullable: false),
                    PhotoUrl = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ContestId = table.Column<Guid>(nullable: false),
                    AllPoints = table.Column<double>(nullable: false)
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
                    Points = table.Column<int>(nullable: false)
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
                    Comment = table.Column<string>(maxLength: 50, nullable: false),
                    Score = table.Column<double>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PhotoId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
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
                    { new Guid("d0a458f4-cba3-4e49-a779-f79a9de41268"), "4b872b7a-fe27-4d89-8e83-0955e6353cde", "Admin", "ADMIN" },
                    { new Guid("8a73d7c7-c092-4281-8cde-6dd9a9dd747c"), "85c41015-26db-44fe-af05-d49969532f2c", "Organizer", "ORGANIZER" },
                    { new Guid("a117e076-855e-401a-aeab-844fee43a0a2"), "8bc49062-17f0-4f26-99fa-97d47894386a", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("dca24186-8ad1-4952-9063-69cebcc1ba06"), new DateTime(2021, 6, 1, 21, 44, 59, 583, DateTimeKind.Utc).AddTicks(1601), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cars" },
                    { new Guid("729b970a-ee54-4852-8ac7-d9b3146e886b"), new DateTime(2021, 6, 1, 21, 44, 59, 583, DateTimeKind.Utc).AddTicks(2212), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Animals" },
                    { new Guid("c7122053-ef1f-48fe-b1d2-891aa1cf0aea"), new DateTime(2021, 6, 1, 21, 44, 59, 583, DateTimeKind.Utc).AddTicks(2242), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nature" },
                    { new Guid("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), new DateTime(2021, 6, 1, 21, 44, 59, 583, DateTimeKind.Utc).AddTicks(2251), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architecture" },
                    { new Guid("fad09db4-8187-4777-9e68-3ba40218c7d3"), new DateTime(2021, 6, 1, 21, 44, 59, 583, DateTimeKind.Utc).AddTicks(2255), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Motorcycles" }
                });

            migrationBuilder.InsertData(
                table: "Ranks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b1728c7-5582-4958-9e97-52c9b1d44cdb"), "Wise and Benevolent Photo Dictator" },
                    { new Guid("a9576301-3157-454f-86ce-85bb5eb2dfc9"), "Master" },
                    { new Guid("41c8e397-f768-48ed-b8f1-f8a238c739b1"), "Enthusiast" },
                    { new Guid("a036e464-8996-4e40-9a81-39239cf72402"), "Admin" },
                    { new Guid("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), "Organizer" },
                    { new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "Junkie" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6"), "Finished" },
                    { new Guid("27c7d81e-eb1c-469b-8919-a532322273cc"), "Phase 2" },
                    { new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186"), "Phase 1" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "OverallPoints", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RankId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"), 0, "14d6aa77-a281-4aeb-9dfc-54d47e8cfc79", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(5351), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "georgi.ivanov@mail.com", false, "Georgi", false, "Ivanov", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GEORGI.IVANOV@MAIL.COM", "GEORGI.IVANOV@MAIL.COM", 0, "AQAAAAEAACcQAAAAEJTE6f3YfogrsMtGeWaINOWQ9j+GTb2PIgnSQodMhYsE2u+YsFaDsdbZuyNeqR9wSg==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E275DD1E24957A7781D42BB68292B", false, "georgi.ivanov@mail.com" },
                    { new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3"), 0, "05a2f877-5afe-4e5f-8af2-7bfd8246f4dd", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(5407), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@mail.com", false, "John", false, "Smith", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JOHN.SMITH@MAIL.COM", "JOHN.SMITH@MAIL.COM", 0, "AQAAAAEAACcQAAAAEBVrPN+SAYvDnS/b/B3ewDmYmPOluQ+T9CVnMoqL8nU+RjDo+umtGkLNPJEAzMT4Qg==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E275DD1E25957A7781D42BB68299B", false, "john.smith@mail.com" },
                    { new Guid("743f0e66-af28-48b9-8322-61395c10207f"), 0, "9694fe58-8876-4300-b5c9-294ac1596a42", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(5424), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "steven.king@mail.com", false, "Steven", false, "King", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "STEVEN.KING@MAIL.COM", "STEVEN.KING@MAIL.COM", 0, "AQAAAAEAACcQAAAAEMkI84vkDCuEihMPt7sUtnVUChkDqJMJqYkIPYyYxelRGMRumgCB+8yUvIOKdTYGug==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E375DD1E25957A7781D42BB68299B", false, "steven.king@mail.com" },
                    { new Guid("71cd9097-0c95-4af2-9e43-da7324880583"), 0, "69fa5a8d-0e1c-4ff6-a4a0-61e3cb670c87", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(5458), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "robert.scott@mail.com", false, "Robert", false, "Scott", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ROBERT.SCOTT@MAIL.COM", "ROBERT.SCOTT@MAIL.COM", 0, "AQAAAAEAACcQAAAAECZqt/2dpehEBmWAitmqWl0ry8MyF+PNs1Bxv1w2vfRrPsvvrFsMqGYzGIAg3G9awQ==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E375DD1E25957A7981D42BB68299B", false, "robert.scott@mail.com" },
                    { new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e"), 0, "d07fc451-3222-4ae1-aa59-e1f145df5e10", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(5475), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jimmy.brown@mail.com", false, "Jimmy", false, "Brown", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JIMMY.BROWN@MAIL.COM", "JIMMY.BROWN@MAIL.COM", 0, "AQAAAAEAACcQAAAAEPSIJsJgiXRBQP+TrGNbLU1jjP7UztVycCIt5JyJKlq+g4J2S3V/4SsJEzaqkWjSXg==", null, false, new Guid("acca215b-d737-406c-b87c-696fb22ce001"), "DC6E375DD1E25957A7981D42BB68399B", false, "jimmy.brown@mail.com" },
                    { new Guid("56763358-b113-4f96-9a4a-5190c421f1fb"), 0, "cbeff77e-29c9-4816-9f6b-4c7c0ba33e7e", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(5492), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sam.stevens@mail.com", false, "Sam", false, "Stevens", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SAM.STEVENS@MAIL.COM", "SAM.STEVENS@MAIL.COM", 200, "AQAAAAEAACcQAAAAEOXBjhT2N+cvQTEt1uEKP7leJfEuJnNh0HhLEwT2QXhCPIm9BBbZAV/sUpd59exA9w==", null, false, new Guid("a9576301-3157-454f-86ce-85bb5eb2dfc9"), "DC6E375DD1E25957A7981D48BB68399B", false, "sam.stevens@mail.com" },
                    { new Guid("c463712b-e235-4fe5-840e-a99736c3fb76"), 0, "e2ae9a08-db9f-4169-9871-b53731f8a6c3", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(6035), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kyle.sins@mail.com", false, "Kyle", false, "Sins", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KYLE.SINS@MAIL.COM", "KYLE.SINS@MAIL.COM", 1200, "AQAAAAEAACcQAAAAEHDUFI8AOfCrH12epIv1sMs6VgLb3XbG1lXfNpzQeuCjJZQ2v7QPR8bYKRT4pqS1kg==", null, false, new Guid("0b1728c7-5582-4958-9e97-52c9b1d44cdb"), "DC6E375DD2E25957A7981D48BB68399B", false, "kyle.sins@mail.com" },
                    { new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), 0, "914aec47-48b8-4e78-b1f7-92f461f07c59", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(5308), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "eric.berg@mail.com", false, "Eric", false, "Berg", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ERIC.BERG@MAIL.COM", "ERIC.BERG@MAIL.COM", 0, "AQAAAAEAACcQAAAAEFNrT/5Oc3YGa6fklMFWlsl2iRi93d6XeQgicGBR7hASZjJSQOZzE8EI4NNsg3gC1w==", null, false, new Guid("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), "DC6E275DD1E24957A7781D42BB68293B", false, "eric.berg@mail.com" },
                    { new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), 0, "bb472483-f599-4742-8ec5-e2ceafa36356", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(6086), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.smith@mail.com", false, "Sara", false, "Smith", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SARA.SMITH@MAIL.COM", "SARA.SMITH@MAIL.COM", 0, "AQAAAAEAACcQAAAAEO64qpmhI20qV7jVRtzUnYdtrw4TsujcN4qMfrh/oDjPItc2y56u2/4qVx+PPKM0Hg==", null, false, new Guid("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), "DC6E275DD1E24917A7781D42BB68293B", false, "sara.smith@mail.com" },
                    { new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), 0, "68e8f006-85ce-4001-993d-0241e96e7ba5", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(6099), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.beck@mail.com", false, "Jane", false, "Beck", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JANE.BECK@MAIL.COM", "JANE.BECK@MAIL.COM", 0, "AQAAAAEAACcQAAAAEHY/nSP57gpfNO2AkzvNZYwiLCSI3DLFkvhSHeA9ya68oz8Ochu1G9PksMApbE8N1A==", null, false, new Guid("0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce"), "DC6E275DD1E24917A7781D42BB64293B", false, "jane.beck@mail.com" },
                    { new Guid("1d4c48e4-8870-417b-8ac6-e78efe1aaab5"), 0, "a893034e-44fd-4dd6-bab2-b469dae4b335", new DateTime(2021, 6, 1, 21, 44, 59, 483, DateTimeKind.Utc).AddTicks(3140), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@mail.com", false, "Admin", false, "Admin", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", 0, "AQAAAAEAACcQAAAAEEZsLjothR3HTYLBeqQUHV/s1H0SJlSITirCA6g0i8iet0VzLVH50k1AHp3RwqqVBQ==", null, false, new Guid("a036e464-8996-4e40-9a81-39239cf72402"), "DC6E275DD1E24957A7781D42BB68299B", false, "admin@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Contests",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "DeletedOn", "Finished", "IsDeleted", "IsOpen", "ModifiedOn", "Name", "Phase1", "Phase2", "StatusId" },
                values: new object[,]
                {
                    { new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new Guid("729b970a-ee54-4852-8ac7-d9b3146e886b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birds", new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(1595), new DateTime(2021, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186") },
                    { new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new Guid("729b970a-ee54-4852-8ac7-d9b3146e886b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 1, 21, 46, 59, 584, DateTimeKind.Utc).AddTicks(915), false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wild cats", new DateTime(2021, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(504), new Guid("27c7d81e-eb1c-469b-8919-a532322273cc") },
                    { new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new Guid("fad09db4-8187-4777-9e68-3ba40218c7d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 9, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Best look", new DateTime(2021, 5, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("27c7d81e-eb1c-469b-8919-a532322273cc") },
                    { new Guid("42541f52-8d30-4828-bf66-4eda82735edd"), new Guid("af4ea8a0-8e69-4746-bbc8-aa4593a11828"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Best building", new DateTime(2021, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6") }
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
                    { new Guid("1d4c48e4-8870-417b-8ac6-e78efe1aaab5"), new Guid("d0a458f4-cba3-4e49-a779-f79a9de41268") },
                    { new Guid("743f0e66-af28-48b9-8322-61395c10207f"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") },
                    { new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") },
                    { new Guid("71cd9097-0c95-4af2-9e43-da7324880583"), new Guid("a117e076-855e-401a-aeab-844fee43a0a2") }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "AllPoints", "ContestId", "CreatedOn", "DeletedOn", "Description", "IsDeleted", "ModifiedOn", "PhotoUrl", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("94499cdd-e18c-4743-b0c4-2e1b7564c46c"), 0.0, new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(4943), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Picture of an eagle.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/eagle.jpg", "Eagle", new Guid("71cd9097-0c95-4af2-9e43-da7324880583") },
                    { new Guid("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"), 0.0, new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(4913), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Picture of a Kawasaki.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/kawasaki.jpg", "Kawasaki Ninja", new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3") },
                    { new Guid("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"), 0.0, new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(4905), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Picture of a tiger.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/tiger.jpg", "Tiger", new Guid("56763358-b113-4f96-9a4a-5190c421f1fb") },
                    { new Guid("507c5f65-497b-4a3c-95f6-cfbc86692ca5"), 0.0, new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(4922), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Picture of a Honda.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/honda.jpg", "Honda CBR", new Guid("c463712b-e235-4fe5-840e-a99736c3fb76") },
                    { new Guid("e165b91f-03bf-414e-88b7-c51b87775683"), 0.0, new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(4781), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Picture of a lion.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/lion.jpg", "Lion King", new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d") },
                    { new Guid("59dd9540-a1d8-4360-99d5-ed8302aae5e2"), 0.0, new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(4930), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Picture of a colibri.", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Images/colibri.jpg", "Collibri", new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e") }
                });

            migrationBuilder.InsertData(
                table: "UserContests",
                columns: new[] { "ContestId", "UserId", "Id", "Points" },
                values: new object[,]
                {
                    { new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new Guid("71cd9097-0c95-4af2-9e43-da7324880583"), new Guid("bb047135-03e9-4957-8248-306eaf8600cc"), 0 },
                    { new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new Guid("021fa300-ffd4-48e2-a93f-d40c17d014f3"), new Guid("d00fb4ba-c05c-4a48-8042-0db3b747b226"), 0 },
                    { new Guid("e2450bf8-c019-4442-a2c3-ed0210586eed"), new Guid("7cc9804e-2106-4943-994d-91be3d1fab8e"), new Guid("1e1008e0-63f6-437a-8c86-347dcf905b7d"), 0 },
                    { new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new Guid("56763358-b113-4f96-9a4a-5190c421f1fb"), new Guid("61f45846-09fd-4112-b3b8-5aaf029e8a9f"), 0 },
                    { new Guid("548873db-705b-46e7-b88d-230c5f06fd35"), new Guid("c463712b-e235-4fe5-840e-a99736c3fb76"), new Guid("f933eff8-9a79-4937-801a-a80aaa8d4b19"), 0 },
                    { new Guid("f36e97ee-98af-4f26-93ef-066895d94b2a"), new Guid("8a20e519-66ad-46b8-b6c3-18c36fa50a1d"), new Guid("be9c8856-5df8-4577-a7c9-f8f62f8de22c"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedOn", "PhotoId", "Score", "UserId", "WrongCategory" },
                values: new object[,]
                {
                    { new Guid("f55244de-da0f-4a9c-b8d9-7940a2f97083"), "Not so great quality of the picture.", new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(7838), new Guid("e165b91f-03bf-414e-88b7-c51b87775683"), 4.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("8198e13a-30cb-4f4b-99f0-acf31a70b02d"), "Great lion.", new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(8377), new Guid("e165b91f-03bf-414e-88b7-c51b87775683"), 8.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("55cf8205-bfb9-4d8c-8ac1-7861a458bb10"), "Very good colour.", new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(8407), new Guid("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"), 9.0, new Guid("a890fe35-c840-4484-bd80-67dbc94ab581"), false },
                    { new Guid("546ff836-f1c5-46e2-ba55-50729daf0419"), "Not a very good setting.", new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(8420), new Guid("fd4b4d23-a4db-4e8b-be63-4af3c4b45757"), 6.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false },
                    { new Guid("73fe1a7a-e31c-4b4e-a6fa-1ae65e7e1f28"), "Marvelous tiger.", new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(8394), new Guid("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"), 10.0, new Guid("e240edfc-64b9-4358-a869-5aadb719e128"), false },
                    { new Guid("b8e942fb-9e23-48b2-b15f-32a1e2c06315"), "Skinny tiger.", new DateTime(2021, 6, 1, 21, 44, 59, 584, DateTimeKind.Utc).AddTicks(8403), new Guid("0fdb02e1-91e1-4132-9ccc-1f73c7f716b9"), 3.0, new Guid("5d608fdc-f7d4-40f2-b052-61a7ea812a23"), false }
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
