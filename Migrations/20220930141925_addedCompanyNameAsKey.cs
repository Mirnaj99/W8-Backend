using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W8_Backend.Migrations
{
    public partial class addedCompanyNameAsKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeMonthlyWorkDifferences",
                table: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "EmployeeMonthlyWorkDifferences",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeMonthlyWorkDifferences",
                table: "EmployeeMonthlyWorkDifferences",
                columns: new[] { "Month", "Year", "Emp_number", "CodeNumber", "CodeDesc", "CompanyName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeMonthlyWorkDifferences",
                table: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "EmployeeMonthlyWorkDifferences",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeMonthlyWorkDifferences",
                table: "EmployeeMonthlyWorkDifferences",
                columns: new[] { "Month", "Year", "Emp_number", "CodeNumber", "CodeDesc" });
        }
    }
}
