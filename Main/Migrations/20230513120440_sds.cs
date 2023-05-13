using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Migrations
{
    /// <inheritdoc />
    public partial class sds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cores_specialization_specializationSpecId",
                table: "cores");

            migrationBuilder.DropIndex(
                name: "IX_cores_specializationSpecId",
                table: "cores");

            migrationBuilder.DropColumn(
                name: "specializationSpecId",
                table: "cores");

            migrationBuilder.CreateIndex(
                name: "IX_cores_SpecId",
                table: "cores",
                column: "SpecId");

            migrationBuilder.AddForeignKey(
                name: "FK_cores_specialization_SpecId",
                table: "cores",
                column: "SpecId",
                principalTable: "specialization",
                principalColumn: "SpecId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cores_specialization_SpecId",
                table: "cores");

            migrationBuilder.DropIndex(
                name: "IX_cores_SpecId",
                table: "cores");

            migrationBuilder.AddColumn<int>(
                name: "specializationSpecId",
                table: "cores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cores_specializationSpecId",
                table: "cores",
                column: "specializationSpecId");

            migrationBuilder.AddForeignKey(
                name: "FK_cores_specialization_specializationSpecId",
                table: "cores",
                column: "specializationSpecId",
                principalTable: "specialization",
                principalColumn: "SpecId");
        }
    }
}
