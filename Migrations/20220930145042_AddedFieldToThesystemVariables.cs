using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W8_Backend.Migrations
{
    public partial class AddedFieldToThesystemVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HRCompanyName",
                table: "SystemVariables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HRJobNb",
                table: "SystemVariables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HRTaskNb",
                table: "SystemVariables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HRCompanyName",
                table: "SystemVariables");

            migrationBuilder.DropColumn(
                name: "HRJobNb",
                table: "SystemVariables");

            migrationBuilder.DropColumn(
                name: "HRTaskNb",
                table: "SystemVariables");
        }
    }
}
