using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Migrations
{
    /// <inheritdoc />
    public partial class D1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseCategories_CourseVideoPlayer_CourseVideoPlayer_classCourseVideoPlayer_Id",
                table: "courseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_teacherProfiles_courseCategories_categorycourseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_teacherProfiles_categorycourseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_courseCategories_CourseVideoPlayer_classCourseVideoPlayer_Id",
                table: "courseCategories");

            migrationBuilder.DropColumn(
                name: "categorycourseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.DropColumn(
                name: "CourseVideoPlayer_classCourseVideoPlayer_Id",
                table: "courseCategories");

            migrationBuilder.CreateIndex(
                name: "IX_teacherProfiles_courseCategory_Id",
                table: "teacherProfiles",
                column: "courseCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_courseCategories_CourseVideoPlayer_Id",
                table: "courseCategories",
                column: "CourseVideoPlayer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_courseCategories_CourseVideoPlayer_CourseVideoPlayer_Id",
                table: "courseCategories",
                column: "CourseVideoPlayer_Id",
                principalTable: "CourseVideoPlayer",
                principalColumn: "CourseVideoPlayer_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teacherProfiles_courseCategories_courseCategory_Id",
                table: "teacherProfiles",
                column: "courseCategory_Id",
                principalTable: "courseCategories",
                principalColumn: "courseCategory_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseCategories_CourseVideoPlayer_CourseVideoPlayer_Id",
                table: "courseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_teacherProfiles_courseCategories_courseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_teacherProfiles_courseCategory_Id",
                table: "teacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_courseCategories_CourseVideoPlayer_Id",
                table: "courseCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "categorycourseCategory_Id",
                table: "teacherProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseVideoPlayer_classCourseVideoPlayer_Id",
                table: "courseCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teacherProfiles_categorycourseCategory_Id",
                table: "teacherProfiles",
                column: "categorycourseCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_courseCategories_CourseVideoPlayer_classCourseVideoPlayer_Id",
                table: "courseCategories",
                column: "CourseVideoPlayer_classCourseVideoPlayer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_courseCategories_CourseVideoPlayer_CourseVideoPlayer_classCourseVideoPlayer_Id",
                table: "courseCategories",
                column: "CourseVideoPlayer_classCourseVideoPlayer_Id",
                principalTable: "CourseVideoPlayer",
                principalColumn: "CourseVideoPlayer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_teacherProfiles_courseCategories_categorycourseCategory_Id",
                table: "teacherProfiles",
                column: "categorycourseCategory_Id",
                principalTable: "courseCategories",
                principalColumn: "courseCategory_Id");
        }
    }
}
