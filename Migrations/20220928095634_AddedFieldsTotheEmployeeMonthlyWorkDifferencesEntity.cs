using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W8_Backend.Migrations
{
    public partial class AddedFieldsTotheEmployeeMonthlyWorkDifferencesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeMonthlyWorkDifferences",
                table: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.RenameColumn(
                name: "Task_number",
                table: "EmployeeMonthlyWorkDifferences",
                newName: "CodeDesc");

            migrationBuilder.AddColumn<string>(
                name: "CodeNumber",
                table: "EmployeeMonthlyWorkDifferences",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "EmployeeMonthlyWorkDifferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsJob",
                table: "EmployeeMonthlyWorkDifferences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSynced",
                table: "EmployeeMonthlyWorkDifferences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeMonthlyWorkDifferences",
                table: "EmployeeMonthlyWorkDifferences",
                columns: new[] { "Month", "Year", "Emp_number", "CodeNumber", "CodeDesc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeMonthlyWorkDifferences",
                table: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.DropColumn(
                name: "CodeNumber",
                table: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.DropColumn(
                name: "IsJob",
                table: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.DropColumn(
                name: "IsSynced",
                table: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.RenameColumn(
                name: "CodeDesc",
                table: "EmployeeMonthlyWorkDifferences",
                newName: "Task_number");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeMonthlyWorkDifferences",
                table: "EmployeeMonthlyWorkDifferences",
                columns: new[] { "Month", "Year", "Emp_number", "Task_number" });
        }
    }
}
