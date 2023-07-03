using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyWaves.Migrations
{
    /// <inheritdoc />
    public partial class profileUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "profile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role_Name = table.Column<string>(type: "nvarchar(max)", nullable: true,defaultValue:"None"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true,defaultValue:"None"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    CardDate = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    CVC = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    Instegram = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    PersonalWebSite = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    Gmail = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None"),
                    Outlook = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "None")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profile");
        }
    }
}
