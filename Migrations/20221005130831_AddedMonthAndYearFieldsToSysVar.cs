using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W8_Backend.Migrations
{
    public partial class AddedMonthAndYearFieldsToSysVar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastSyncMonth",
                table: "SystemVariables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastSyncYear",
                table: "SystemVariables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSyncMonth",
                table: "SystemVariables");

            migrationBuilder.DropColumn(
                name: "LastSyncYear",
                table: "SystemVariables");
        }
    }
}
