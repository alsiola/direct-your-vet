using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class prac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientPractices_Practice_PracticeId",
                table: "ClientPractices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Practice",
                table: "Practice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Practices",
                table: "Practice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientPractices_Practices_PracticeId",
                table: "ClientPractices",
                column: "PracticeId",
                principalTable: "Practice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameTable(
                name: "Practice",
                newName: "Practices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientPractices_Practices_PracticeId",
                table: "ClientPractices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Practices",
                table: "Practices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Practice",
                table: "Practices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientPractices_Practice_PracticeId",
                table: "ClientPractices",
                column: "PracticeId",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameTable(
                name: "Practices",
                newName: "Practice");
        }
    }
}
