using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W8_Backend.Migrations
{
    public partial class AddedFieldsToSysVar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeleteRecords",
                table: "SystemVariables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PostingDateToDelete",
                table: "SystemVariables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteRecords",
                table: "SystemVariables");

            migrationBuilder.DropColumn(
                name: "PostingDateToDelete",
                table: "SystemVariables");
        }
    }
}
