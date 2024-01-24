using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W8_Backend.Migrations
{
    public partial class modifiedFieldsInSysVar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSyncMonth",
                table: "SystemVariables");

            migrationBuilder.DropColumn(
                name: "LastSyncYear",
                table: "SystemVariables");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncDate",
                table: "SystemVariables",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSyncDate",
                table: "SystemVariables");

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
    }
}
