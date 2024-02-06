using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolApi.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyPhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyEmail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseSchedules",
                columns: table => new
                {
                    CourseId = table.Column<long>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSchedules", x => new { x.CourseId, x.Date });
                    table.ForeignKey(
                        name: "FK_CourseSchedules_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseParticipants",
                columns: table => new
                {
                    CourseId = table.Column<long>(type: "INTEGER", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseParticipants", x => new { x.CourseId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_CourseParticipants_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseParticipants_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "CompanyEmail", "CompanyName", "CompanyPhoneNumber", "Name" },
                values: new object[,]
                {
                    { 1L, null, null, null, "Cutting trees, the ins and outs" },
                    { 2L, null, null, null, "CSS and you - a love story" },
                    { 3L, null, null, null, "Baking mud cakes using actual mud" },
                    { 4L, null, null, null, "Christmas eve - myth or reality?" },
                    { 5L, null, null, null, "LEGO colors through time" }
                });

            migrationBuilder.InsertData(
                table: "CourseSchedules",
                columns: new[] { "CourseId", "Date" },
                values: new object[,]
                {
                    { 1L, new DateOnly(2017, 1, 1) },
                    { 1L, new DateOnly(2017, 10, 31) },
                    { 2L, new DateOnly(2017, 5, 25) },
                    { 2L, new DateOnly(2017, 5, 26) },
                    { 2L, new DateOnly(2017, 5, 27) },
                    { 3L, new DateOnly(2017, 1, 1) },
                    { 3L, new DateOnly(2017, 4, 1) },
                    { 3L, new DateOnly(2018, 12, 10) },
                    { 3L, new DateOnly(2019, 3, 12) },
                    { 4L, new DateOnly(2017, 12, 24) },
                    { 4L, new DateOnly(2018, 12, 24) },
                    { 4L, new DateOnly(2019, 12, 24) },
                    { 5L, new DateOnly(2017, 6, 30) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseParticipants_ParticipantId",
                table: "CourseParticipants",
                column: "ParticipantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseParticipants");

            migrationBuilder.DropTable(
                name: "CourseSchedules");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
