using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W8_Backend.Migrations
{
    public partial class modifiedFieldsLabelInSysVar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostingDateToDelete",
                table: "SystemVariables",
                newName: "HRCompanyCode");

            migrationBuilder.RenameColumn(
                name: "HRCompanyName",
                table: "SystemVariables",
                newName: "DocumentNumberToDelete");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HRCompanyCode",
                table: "SystemVariables",
                newName: "PostingDateToDelete");

            migrationBuilder.RenameColumn(
                name: "DocumentNumberToDelete",
                table: "SystemVariables",
                newName: "HRCompanyName");
        }
    }
}
