using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class subuserinsmsgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SMSGroupSendResults");

            migrationBuilder.AddColumn<string>(
                name: "SubscriberUserId",
                table: "SMSGroupSendResults",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SMSGroupSendResults_SubscriberUserId",
                table: "SMSGroupSendResults",
                column: "SubscriberUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SMSGroupSendResults_AspNetUsers_SubscriberUserId",
                table: "SMSGroupSendResults",
                column: "SubscriberUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SMSGroupSendResults_AspNetUsers_SubscriberUserId",
                table: "SMSGroupSendResults");

            migrationBuilder.DropIndex(
                name: "IX_SMSGroupSendResults_SubscriberUserId",
                table: "SMSGroupSendResults");

            migrationBuilder.DropColumn(
                name: "SubscriberUserId",
                table: "SMSGroupSendResults");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SMSGroupSendResults",
                nullable: true);
        }
    }
}
