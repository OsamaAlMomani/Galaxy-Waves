using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyWaves.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guide_equipment_equipmentId",
                table: "guide");

            migrationBuilder.CreateTable(
                name: "guideEquipment",
                columns: table => new
                {
                    equipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    equipmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    equipmentImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    equipmentColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    equipmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    equipmentBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guideEquipment", x => x.equipmentId);
                });

            migrationBuilder.CreateTable(
                name: "RegisterViewModel",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordConfirmation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterViewModel", x => x.EmailAddress);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_guide_guideEquipment_equipmentId",
                table: "guide",
                column: "equipmentId",
                principalTable: "guideEquipment",
                principalColumn: "equipmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guide_guideEquipment_equipmentId",
                table: "guide");

            migrationBuilder.DropTable(
                name: "guideEquipment");

            migrationBuilder.DropTable(
                name: "RegisterViewModel");

            migrationBuilder.AddForeignKey(
                name: "FK_guide_equipment_equipmentId",
                table: "guide",
                column: "equipmentId",
                principalTable: "equipment",
                principalColumn: "equipmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
