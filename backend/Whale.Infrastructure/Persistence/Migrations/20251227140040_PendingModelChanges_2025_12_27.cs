using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whale.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PendingModelChanges_2025_12_27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Status",
                table: "Users",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherProfiles_VerificationStatus",
                table: "TeacherProfiles",
                column: "VerificationStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Status",
                table: "Courses",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Status",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TeacherProfiles_VerificationStatus",
                table: "TeacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Status",
                table: "Courses");
        }
    }
}
