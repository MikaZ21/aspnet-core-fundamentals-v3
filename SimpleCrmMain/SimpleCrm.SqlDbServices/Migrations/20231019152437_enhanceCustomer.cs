using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleCrm.SqlDbServices.Migrations
{
    public partial class enhanceCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Customer",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastContactDate",
                table: "Customer",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PreferredContactMethod",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastContactDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PreferredContactMethod",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Customer");
        }
    }
}
