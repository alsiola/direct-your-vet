using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class eh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriberPractice_Practices_PracticeId",
                table: "SubscriberPractice");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriberPractice_AspNetUsers_SubscriberUserId",
                table: "SubscriberPractice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriberPractice",
                table: "SubscriberPractice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriberPractices",
                table: "SubscriberPractice",
                columns: new[] { "PracticeId", "SubscriberUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriberPractices_Practices_PracticeId",
                table: "SubscriberPractice",
                column: "PracticeId",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriberPractices_AspNetUsers_SubscriberUserId",
                table: "SubscriberPractice",
                column: "SubscriberUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_SubscriberPractice_SubscriberUserId",
                table: "SubscriberPractice",
                newName: "IX_SubscriberPractices_SubscriberUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriberPractice_PracticeId",
                table: "SubscriberPractice",
                newName: "IX_SubscriberPractices_PracticeId");

            migrationBuilder.RenameTable(
                name: "SubscriberPractice",
                newName: "SubscriberPractices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriberPractices_Practices_PracticeId",
                table: "SubscriberPractices");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriberPractices_AspNetUsers_SubscriberUserId",
                table: "SubscriberPractices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriberPractices",
                table: "SubscriberPractices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriberPractice",
                table: "SubscriberPractices",
                columns: new[] { "PracticeId", "SubscriberUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriberPractice_Practices_PracticeId",
                table: "SubscriberPractices",
                column: "PracticeId",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriberPractice_AspNetUsers_SubscriberUserId",
                table: "SubscriberPractices",
                column: "SubscriberUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_SubscriberPractices_SubscriberUserId",
                table: "SubscriberPractices",
                newName: "IX_SubscriberPractice_SubscriberUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriberPractices_PracticeId",
                table: "SubscriberPractices",
                newName: "IX_SubscriberPractice_PracticeId");

            migrationBuilder.RenameTable(
                name: "SubscriberPractices",
                newName: "SubscriberPractice");
        }
    }
}
