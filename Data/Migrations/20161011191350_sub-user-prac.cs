using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class subuserprac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriberPractice",
                columns: table => new
                {
                    PracticeId = table.Column<int>(nullable: false),
                    SubscriberUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberPractice", x => new { x.PracticeId, x.SubscriberUserId });
                    table.ForeignKey(
                        name: "FK_SubscriberPractice_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriberPractice_AspNetUsers_SubscriberUserId",
                        column: x => x.SubscriberUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberPractice_PracticeId",
                table: "SubscriberPractice",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberPractice_SubscriberUserId",
                table: "SubscriberPractice",
                column: "SubscriberUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriberPractice");
        }
    }
}
