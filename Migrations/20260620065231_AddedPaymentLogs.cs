using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerCopilot.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedPaymentLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastResetDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProposalsUsedThisMonth",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastResetDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProposalsUsedThisMonth",
                table: "AspNetUsers");
        }
    }
}
