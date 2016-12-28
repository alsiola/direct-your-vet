using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class smssignuptracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RecipientSignedUp",
                table: "SMSSendResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SlugOpened",
                table: "SMSSendResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UniqueSignupSlug",
                table: "SMSSendResult",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientSignedUp",
                table: "SMSSendResult");

            migrationBuilder.DropColumn(
                name: "SlugOpened",
                table: "SMSSendResult");

            migrationBuilder.DropColumn(
                name: "UniqueSignupSlug",
                table: "SMSSendResult");
        }
    }
}
