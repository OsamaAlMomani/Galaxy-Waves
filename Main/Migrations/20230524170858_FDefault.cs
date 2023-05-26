using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Migrations
{
    /// <inheritdoc />
    public partial class FDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Twitter",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PersonalWebSite",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Outlook",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Facebook",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DOB",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "0",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardHolderName",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardDate",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CVC",
                table: "profile",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Twitter",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalWebSite",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "Outlook",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "Facebook",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "DOB",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "0");

            migrationBuilder.AlterColumn<string>(
                name: "CardHolderName",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "CardDate",
                table: "profile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<int>(
                name: "CVC",
                table: "profile",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);
        }
    }
}
