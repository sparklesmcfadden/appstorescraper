using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStoreScraperModels.Migrations
{
    public partial class AddingDateLoaded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateLoaded",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLoaded",
                table: "Reviews");
        }
    }
}
