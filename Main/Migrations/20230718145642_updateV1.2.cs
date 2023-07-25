using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Migrations
{
    /// <inheritdoc />
    public partial class updateV12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.CreateTable(
                name: "teacherProfiles",
                columns: table => new
                {
                    TeacherProfile_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherProfile_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherProfile_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherProfile_Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacherProfiles", x => x.TeacherProfile_Id);
                });

            migrationBuilder.CreateTable(
                name: "courseCategories",
                columns: table => new
                {
                    courseCategory_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    courseCategory_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseCategory_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseCategory_Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseCategory_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TeacherProfile_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherProfile_Id1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseCategories", x => x.courseCategory_Id);
                    table.ForeignKey(
                        name: "FK_courseCategories_cores_CoreId",
                        column: x => x.CoreId,
                        principalTable: "cores",
                        principalColumn: "CoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_courseCategories_teacherProfiles_TeacherProfile_Id1",
                        column: x => x.TeacherProfile_Id1,
                        principalTable: "teacherProfiles",
                        principalColumn: "TeacherProfile_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_courseCategories_CoreId",
                table: "courseCategories",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_courseCategories_TeacherProfile_Id1",
                table: "courseCategories",
                column: "TeacherProfile_Id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseCategories");

            migrationBuilder.DropTable(
                name: "teacherProfiles");

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IBan = table.Column<long>(type: "bigint", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });
        }
    }
}
