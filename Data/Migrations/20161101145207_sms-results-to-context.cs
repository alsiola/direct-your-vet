using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class smsresultstocontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SMSSendResult_SMSGroupSendResults_SMSGroupSendResultId",
                table: "SMSSendResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SMSSendResult",
                table: "SMSSendResult");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SMSSendResults",
                table: "SMSSendResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SMSSendResults_SMSGroupSendResults_SMSGroupSendResultId",
                table: "SMSSendResult",
                column: "SMSGroupSendResultId",
                principalTable: "SMSGroupSendResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_SMSSendResult_SMSGroupSendResultId",
                table: "SMSSendResult",
                newName: "IX_SMSSendResults_SMSGroupSendResultId");

            migrationBuilder.RenameTable(
                name: "SMSSendResult",
                newName: "SMSSendResults");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SMSSendResults_SMSGroupSendResults_SMSGroupSendResultId",
                table: "SMSSendResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SMSSendResults",
                table: "SMSSendResults");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SMSSendResult",
                table: "SMSSendResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SMSSendResult_SMSGroupSendResults_SMSGroupSendResultId",
                table: "SMSSendResults",
                column: "SMSGroupSendResultId",
                principalTable: "SMSGroupSendResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_SMSSendResults_SMSGroupSendResultId",
                table: "SMSSendResults",
                newName: "IX_SMSSendResult_SMSGroupSendResultId");

            migrationBuilder.RenameTable(
                name: "SMSSendResults",
                newName: "SMSSendResult");
        }
    }
}
