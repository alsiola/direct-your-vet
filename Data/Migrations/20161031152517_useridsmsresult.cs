using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class useridsmsresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "SMSGroupSendResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SMSGroupSendResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                table: "SMSGroupSendResults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SMSGroupSendResults");
        }
    }
}
