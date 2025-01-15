using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot.Migrations
{
    /// <inheritdoc />
    public partial class WhoCares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Schedule");

            migrationBuilder.AddColumn<List<string>>(
                name: "GroupNames",
                table: "Schedule",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupNames",
                table: "Schedule");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Schedule",
                type: "text",
                nullable: true);
        }
    }
}
