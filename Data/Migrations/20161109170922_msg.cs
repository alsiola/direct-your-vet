using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class msg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageGroupResult_Practices_PracticeId1",
                table: "MessageGroupResult");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageSendResult_MessageGroupResult_GroupSendResultId",
                table: "MessageSendResult");

            migrationBuilder.DropIndex(
                name: "IX_MessageGroupResult_PracticeId1",
                table: "MessageGroupResult");

            migrationBuilder.DropColumn(
                name: "PracticeId1",
                table: "MessageGroupResult");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSendResult_MessageGroupResult_GroupSendResultId",
                table: "MessageSendResult",
                column: "GroupSendResultId",
                principalTable: "MessageGroupResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageSendResult_MessageGroupResult_GroupSendResultId",
                table: "MessageSendResult");

            migrationBuilder.AddColumn<int>(
                name: "PracticeId1",
                table: "MessageGroupResult",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageGroupResult_PracticeId1",
                table: "MessageGroupResult",
                column: "PracticeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageGroupResult_Practices_PracticeId1",
                table: "MessageGroupResult",
                column: "PracticeId1",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSendResult_MessageGroupResult_GroupSendResultId",
                table: "MessageSendResult",
                column: "GroupSendResultId",
                principalTable: "MessageGroupResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
