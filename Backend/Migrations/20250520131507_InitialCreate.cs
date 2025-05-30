using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    courseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseDuration = table.Column<int>(type: "int", nullable: false),
                    courseLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.courseId);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    opportunityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    opportunityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opportunityCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opportunityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opportunityLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opportunityImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opportunityCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    opportunityDeadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.opportunityId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isProfileComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    graduationYear = table.Column<int>(type: "int", nullable: false),
                    department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    section = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    rollNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    leetcodeUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    githubUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    codechefUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    hackerrankUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    codeforcesUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    gfgUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    interests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CodingPlatformStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    platformUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    EasySolved = table.Column<int>(type: "int", nullable: false),
                    MediumSolved = table.Column<int>(type: "int", nullable: false),
                    HardSolved = table.Column<int>(type: "int", nullable: false),
                    TotalSolved = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TotalSubmissions = table.Column<int>(type: "int", nullable: false),
                    TotalContests = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    MaxRating = table.Column<int>(type: "int", nullable: false),
                    Ranking = table.Column<long>(type: "bigint", nullable: false),
                    GlobalRank = table.Column<int>(type: "int", nullable: false),
                    CountryRank = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicRepos = table.Column<int>(type: "int", nullable: false),
                    Followers = table.Column<int>(type: "int", nullable: false),
                    Following = table.Column<int>(type: "int", nullable: false),
                    ContributionsLastYear = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingPlatformStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodingPlatformStats_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CodingPlatformStats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodingPlatformStats_UserId",
                table: "CodingPlatformStats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingPlatformStats_UserProfileId",
                table: "CodingPlatformStats",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodingPlatformStats");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
