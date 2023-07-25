using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Migrations
{
    /// <inheritdoc />
    public partial class E1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teacherProfiles_courseCategories_courseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_teacherProfiles_courseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.AddColumn<Guid>(
                name: "categorycourseCategory_Id",
                table: "teacherProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teacherProfiles_categorycourseCategory_Id",
                table: "teacherProfiles",
                column: "categorycourseCategory_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_teacherProfiles_courseCategories_categorycourseCategory_Id",
                table: "teacherProfiles",
                column: "categorycourseCategory_Id",
                principalTable: "courseCategories",
                principalColumn: "courseCategory_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teacherProfiles_courseCategories_categorycourseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_teacherProfiles_categorycourseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.DropColumn(
                name: "categorycourseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.CreateIndex(
                name: "IX_teacherProfiles_courseCategory_Id",
                table: "teacherProfiles",
                column: "courseCategory_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_teacherProfiles_courseCategories_courseCategory_Id",
                table: "teacherProfiles",
                column: "courseCategory_Id",
                principalTable: "courseCategories",
                principalColumn: "courseCategory_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
