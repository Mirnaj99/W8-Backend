using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W8_Backend.Migrations
{
    public partial class InitialMigratio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NavConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMonthlyWorkDifferences",
                columns: table => new
                {
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Emp_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Task_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Total_minus_month = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Working_hours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percent_working = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percent_of_month = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPerMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMonthlyWorkDifferences", x => new { x.Month, x.Year, x.Emp_number, x.Task_number });
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LogEndDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "SystemVariables",
                columns: table => new
                {
                    VariableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyncStatus = table.Column<bool>(type: "bit", nullable: false),
                    IsSyncing = table.Column<bool>(type: "bit", nullable: false),
                    SyncMonthlyCostSheet = table.Column<bool>(type: "bit", nullable: false),
                    LogCleanDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Retention = table.Column<int>(type: "int", nullable: false),
                    LogCleanRuntime = table.Column<DateTime>(type: "datetime", nullable: true),
                    MonthlyCostSheetPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetPathForMonthlyCostSheet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Api1Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Api2Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemVariables", x => x.VariableID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MicroUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "LogDetails",
                columns: table => new
                {
                    DetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogDetailTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogDetails", x => x.DetailID);
                    table.ForeignKey(
                        name: "FK_LogDetails_Logs_LogID",
                        column: x => x.LogID,
                        principalTable: "Logs",
                        principalColumn: "LogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogDetails_LogID",
                table: "LogDetails",
                column: "LogID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MicroUserID",
                table: "Users",
                column: "MicroUserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "EmployeeMonthlyWorkDifferences");

            migrationBuilder.DropTable(
                name: "LogDetails");

            migrationBuilder.DropTable(
                name: "SystemVariables");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
