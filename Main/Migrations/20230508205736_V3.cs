using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "specialization",
                columns: table => new
                {
                    SpecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialization", x => x.SpecId);
                    table.ForeignKey(
                        name: "FK_specialization_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cores",
                columns: table => new
                {
                    CoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecId = table.Column<int>(type: "int", nullable: false),
                    specializationSpecId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cores", x => x.CoreId);
                    table.ForeignKey(
                        name: "FK_cores_specialization_specializationSpecId",
                        column: x => x.specializationSpecId,
                        principalTable: "specialization",
                        principalColumn: "SpecId");
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_course_Teacher_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_cores_CoreId",
                        column: x => x.CoreId,
                        principalTable: "cores",
                        principalColumn: "CoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cores_specializationSpecId",
                table: "cores",
                column: "specializationSpecId");

            migrationBuilder.CreateIndex(
                name: "IX_course_CoreId",
                table: "course",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_course_TeacherID",
                table: "course",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_specialization_CategoryId",
                table: "specialization",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "cores");

            migrationBuilder.DropTable(
                name: "specialization");
        }
    }
}
